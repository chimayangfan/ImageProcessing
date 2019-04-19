using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace TreeListTools
{
    public static class TreeListTool
    {
        public delegate string BuildPathRule(string nodeText, string fullPathInfo);
        /// <summary>
        /// 获取选中节点到根节点的所有信息
        /// </summary>
        /// <param name="focusedNode">TreeListNode</param>
        /// <param name="columnID">列名称</param>
        /// <param name="buildPathRule">规则委托</param>
        /// <returns>路径信息</returns>
        public static string FullPathInfo(this TreeListNode focusedNode, string columnID, BuildPathRule buildPathRule)
        {
            if (focusedNode == null)
                throw new ArgumentNullException("focusedNode");
            if (string.IsNullOrEmpty("columnID"))
                throw new ArgumentNullException("columnID");
            string _fullPathInfo = string.Empty;
            _fullPathInfo = focusedNode.GetDisplayText(columnID);
            while (focusedNode.ParentNode != null)
            {
                focusedNode = focusedNode.ParentNode;
                string _nodeText = focusedNode.GetDisplayText(columnID).Trim();
                _fullPathInfo = buildPathRule(_nodeText, _fullPathInfo);
            }
            return _fullPathInfo;
        }
        public delegate bool CompareNodeRule(TreeListNode focusedNode);
        /// <summary>
        /// 获取筛选节点到根节点的所有信息
        /// </summary>
        /// <param name="focusedNode">TreeListNode</param>
        /// <param name="columnID">列名称</param>
        /// <param name="compareNodeRule">规则委托</param>
        /// <param name="buildPathRule">规则委托</param>
        /// <returns>路径信息</returns>
        public static string FilterPathInfo(this TreeListNode focusedNode, string columnID, CompareNodeRule compareNodeRule, BuildPathRule buildPathRule)
        {
            if (focusedNode == null)
                throw new ArgumentNullException("focusedNode");
            if (string.IsNullOrEmpty("columnID"))
                throw new ArgumentNullException("columnID");
            string _fullPathInfo = string.Empty;
            _fullPathInfo = focusedNode.GetDisplayText(columnID);
            while (focusedNode.ParentNode != null)
            {
                focusedNode = focusedNode.ParentNode;
                if (compareNodeRule(focusedNode))
                {
                    string _nodeText = focusedNode.GetDisplayText(columnID).Trim();
                    _fullPathInfo = buildPathRule(_nodeText, _fullPathInfo);
                }
            }
            return _fullPathInfo;
        }
        /// <summary>
        /// 递归遍历树节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="opreateRule"></param>
        public static void LoopTree(this TreeList tree, Action<TreeListNode> opreateRule)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");
            foreach (TreeListNode node in tree.Nodes)
            {
                opreateRule(node);
                if (node.Nodes.Count > 0)
                {
                    LoopTreeNodes(node, opreateRule);
                }
            }
        }
        /// <summary>
        /// 递归遍历TreeListNode节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="opreateRule"></param>
        public static void LoopTreeNodes(this TreeListNode node, Action<TreeListNode> opreateRule)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            foreach (TreeListNode _childNode in node.Nodes)
            {
                opreateRule(_childNode);
                LoopTreeNodes(_childNode, opreateRule);
            }
        }
        /// <summary>
        /// 递归遍历TreeListNode,当opreateRule返回false停止循环
        /// </summary>
        /// <param name="node">TreeListNode</param>
        /// <param name="opreateRule">Func<TreeListNode, bool></param>
        public static void LoopTreeNodes_Break(this TreeListNode node, Func<TreeListNode, bool> opreateRule)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            foreach (TreeListNode _childNode in node.Nodes)
            {
                if (!opreateRule(_childNode))
                    break;
                LoopTreeNodes_Break(_childNode, opreateRule);
            }
        }
        /// <summary>
        /// 递归遍历TreeListNode,当opreateRule返回false跳出循环，直接进入下次循环
        /// </summary>
        /// <param name="node">TreeListNode</param>
        /// <param name="opreateRule">Func<TreeListNode, bool></param>
        public static void LoopTreeNodes_Continue(this TreeListNode node, Func<TreeListNode, bool> opreateRule)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            foreach (TreeListNode _childNode in node.Nodes)
            {
                if (!opreateRule(_childNode))
                    continue;
                LoopTreeNodes_Continue(_childNode, opreateRule);
            }
        }
        public delegate bool CheckNodeRule(TreeListNode fucusedNode);
        public delegate void CheckNodeNullRule();
        /// <summary>
        /// 节点为null检查
        /// </summary>
        /// <param name="fucusedNode">TreeListNode</param>
        /// <param name="checkNodeRule">若为NULL,处理逻辑</param>
        /// <returns>TreeListNode</returns>
        public static TreeListNode CheckNull(this TreeListNode fucusedNode, CheckNodeNullRule checkNodeRule)
        {
            if (fucusedNode == null)
            {
                checkNodeRule();
                return null;
            }
            return fucusedNode;
        }
        /// <summary>
        /// 正对节点的检查逻辑
        /// </summary>
        /// <param name="fucusedNode">TreeListNode</param>
        /// <param name="checkNodeRule">检查逻辑代码[委托]</param>
        /// <returns>TreeListNode</returns>
        public static TreeListNode Check(this TreeListNode fucusedNode, CheckNodeRule checkNodeRule)
        {
            if (fucusedNode != null)
                return checkNodeRule(fucusedNode) == true ? fucusedNode : null;
            return null;
        }
        /// <summary>
        /// 水平滚动条
        /// </summary>
        /// <param name="tree">TreeList</param>
        public static void HorzScroll(this TreeList tree)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");
            tree.OptionsView.AutoWidth = false;
            tree.BestFitColumns();
            tree.HorzScrollVisibility = ScrollVisibility.Always;
        }
        /// <summary>
        /// 为TreeList附加右键菜单
        /// MouseUp(object sender, MouseEventArgs e)事件中调用
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="menu">PopupMenu</param>
        /// <param name="attachMenuRule">AttachMenuRule</param>
        public static void AttachMenu(this TreeList tree, MouseEventArgs e, PopupMenu menu, Func<TreeListNode, bool> attachMenuRule)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");
            if (menu == null)
                throw new ArgumentNullException("menu");
            if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.None && tree.State == TreeListState.Regular)
            {
                Point _point = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo _hitInfo = tree.CalcHitInfo(e.Location);
                if (_hitInfo.HitInfoType == HitInfoType.Cell)
                    tree.SetFocusedNode(_hitInfo.Node);
                if (attachMenuRule(tree.FocusedNode))
                    menu.ShowPopup(_point);
            }
        }
        /// <summary>
        /// 设置父节点的状态AfterCheckNode(object sender, NodeEventArgs e)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        public static void ProcessNodeCheckState(this TreeListNode node, CheckState check)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            SetCheckedChildNodes(node, check);
            SetCheckedParentNodes(node, check);
        }
        /// <summary>
        /// 设置子节点CheckState
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private static void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            if (node != null)
            {
                node.LoopTreeNodes((TreeListNode _node) =>
                {
                    _node.CheckState = check;
                });
            }
        }
        /// <summary>
        /// 设置父节点CheckState
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private static void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool _checkStatus = false;
                CheckState _nodeState;
                node.LoopTreeNodes_Break((TreeListNode _node) =>
                {
                    _nodeState = _node.CheckState;
                    if (!check.Equals(_nodeState))
                    {
                        _checkStatus = !_checkStatus;
                        return false;//跳出循环
                    }
                    return true;//继续循环
                });
                node.ParentNode.CheckState = _checkStatus ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }
        /// <summary>
        /// 根据CheckState获取TreeListNode
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="state">CheckState</param>
        /// <param name="GetNodesByStateRule">返回True的时候继续</param>
        /// <returns>TreeListNode集合</returns>
        public static List<TreeListNode> GetNodesByState(this TreeList tree, CheckState state, Func<TreeListNode, bool> GetNodesByStateRule)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");
            List<TreeListNode> _checkNodes = new List<TreeListNode>();
            tree.LoopTree((TreeListNode node) =>
            {
                if (GetNodesByStateRule(node))
                {
                    if (node.CheckState == state)
                        _checkNodes.Add(node);
                }
            });
            return _checkNodes;
        }
    }
}
