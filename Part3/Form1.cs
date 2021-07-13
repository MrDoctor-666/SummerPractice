using System;
using System.Drawing;
using System.Windows.Forms;

namespace Part3
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FracTree tree = new FracTree(70, new Point(90, 90));
            bool drawLine = checkBox1.Checked;
            tree.BuildFracTree((int)numericUpDown1.Value);
            Graphics graphics = pictureBox1.CreateGraphics();
            tree.Draw(graphics, checkBox1.Checked);
        }
    }

    public class FracTree
    {
        public class TreeNode
        {
            public int position { get; } //left up, right up, right down, left down
            public int level;
            public Point center;
            public float a { get; set; }
            public TreeNode parent { get; set; }
            public TreeNode[] children;
            public TreeNode(TreeNode parent, int pos)
            {
                children = new TreeNode[4];
                position = pos;
                this.parent = parent;
                if (parent != null)
                {
                    a = parent.a / 2;
                    switch (position)
                    {
                        case 0: center = new Point((int)(parent.center.X - parent.a / 2), (int)(parent.center.Y - parent.a / 2)); break;
                        case 1: center = new Point((int)(parent.center.X + parent.a / 2), (int)(parent.center.Y - parent.a / 2)); break;
                        case 2: center = new Point((int)(parent.center.X + parent.a / 2), (int)(parent.center.Y + parent.a / 2)); break;
                        case 3: center = new Point((int)(parent.center.X - parent.a / 2), (int)(parent.center.Y + parent.a / 2)); break;
                    }
                    level = parent.level + 1;
                }
            }
        }

        TreeNode root;
        int levels;
        bool drawLine = false;

        public FracTree(int a, Point center)
        {
            root = new TreeNode(null, -1);
            root.a = a; root.center = center;
            root.level = 0;
            levels = 0;
        }

        public void BuildFracTree(int levels)
        {
            this.levels = levels;
            if (levels > 0) BuildRecurs(root);
        }

        private void BuildRecurs(TreeNode cur)
        {
            for (int i = 0; i < 4; i++)
            {
                bool needToDraw = !((cur != root) && ((cur.position % 2 == 0 && (i + cur.position == 2)) || (cur.position % 2 != 0 && (i + cur.position == 4))));
                if (needToDraw)
                {
                    cur.children[i] = new TreeNode(cur, i);
                    if (cur.children[i].level != levels) BuildRecurs(cur.children[i]);
                }
                else cur.children[i] = null;
            }
        }

        public void Draw(Graphics gr, bool drawLine = false)
        {
            this.drawLine = drawLine;
            gr.Clear(Color.White);
            DrawRecurs(root, gr);
            float centerX = gr.VisibleClipBounds.Width / 2, centerY = 2 * (2 * root.a + 20) + 20;
            DrawTreeRecurs(gr, root, new PointF(centerX, centerY));

        }
        void DrawRecurs(TreeNode cur, Graphics gr)
        {
            for (int i = 0; i < 4; i++)
            {
                TreeNode child = cur.children[i];
                if (child != null)
                {
                    bool needToDraw = !((cur != root) && ((cur.position % 2 == 0 && child.position + cur.position == 2) || (cur.position % 2 != 0 && child.position + cur.position == 4)));
                    if (needToDraw) DrawRecurs(child, gr);
                }
            }

            float moveX = 2 * root.a + 5, moveY = 0, k;
            for (int i = cur.level; i <= levels; i++)
            {
                k = i;
                if (i > 4) { moveY = 2 * root.a + 20; k = k % 5; }
                gr.FillRectangle(new SolidBrush(ChooseColor(cur)), (float)(cur.center.X + moveX*k - cur.a / 2), (float)(cur.center.Y + moveY - cur.a / 2), cur.a, cur.a);
                if(drawLine) gr.DrawRectangle(new Pen(Color.Black), (float)(cur.center.X + moveX*k - cur.a / 2), (float)(cur.center.Y + moveY - cur.a / 2), cur.a, cur.a);
            }
        }

        void DrawTreeRecurs(Graphics gr, TreeNode cur, PointF parCenter)
        {
            int koeff = -1; float width = gr.VisibleClipBounds.Width / 2;
            for (int i = 0; i < 4; i++)
            {
                TreeNode child = cur.children[i];
                PointF newCenter = new PointF(0, 0);
                if (cur == root) newCenter = new PointF(parCenter.X * 2 * (i + 1) / 5, parCenter.Y + 20);
                else if (child != null) newCenter = new PointF(parCenter.X + koeff*width/(4*(float)Math.Pow(3, cur.children[i].level-1)), parCenter.Y + 20);
                if (child != null) { koeff++; DrawTreeRecurs(gr, child, newCenter); gr.DrawLine(new Pen(ChooseColor(cur)), parCenter.X + 2, parCenter.Y + 2, newCenter.X + 2, newCenter.Y + 2); }
            }

            SolidBrush brush = new SolidBrush(ChooseColor(cur));
            gr.FillEllipse(brush, parCenter.X, parCenter.Y, 5, 5);
            
        }

        Color ChooseColor(TreeNode node)
        {
            //Random rand = new Random();
            //return Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            switch (node.level)
            {
                case 0: return Color.Red;
                case 1: return Color.Orange;
                case 2: return Color.Yellow;
                case 3: return Color.Green;
                case 4: return Color.LightBlue;
                case 5: return Color.Blue;
                case 6: return Color.Purple;
                case 7: return Color.Pink;
                case 8: return Color.Crimson;
                case 9: return Color.Firebrick;
                default: return Color.Black;
            }
        }
    }
}
