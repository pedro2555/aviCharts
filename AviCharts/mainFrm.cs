using PdfiumViewer;
using System;
using System.IO;
using System.Windows.Forms;

namespace AviCharts
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();

            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory + "\\CHARTS");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
                rootNode.Expand();
            }

        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";

                GetFiles(subDir, aNode);

                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void GetFiles(DirectoryInfo dir, TreeNode nodeToAdd)
        {
            foreach (FileInfo file in dir.GetFiles("*.pdf"))
            {
                nodeToAdd.Nodes.Add(
                    file.FullName,
                    file.Name.Substring(file.Name.IndexOf('.') + 1, file.Name.Length - file.Name.IndexOf('.') - 5));
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                pdfViewer.Document = PdfDocument.Load((e.Node.Nodes.Count == 0) ? e.Node.Name : e.Node.Nodes[0].Name);
                pdfViewer.Focus();
            }
            catch (Exception crap)
            {
                pdfViewer.Document = null;
            }
        }
    }
}
