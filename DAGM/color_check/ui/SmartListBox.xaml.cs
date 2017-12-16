using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DAGM.ui
{
    /// <summary>
    /// Interaction logic for SmartListBox.xaml
    /// </summary>
    public partial class SmartListBox : UserControl
    {
        private bool AvailableDrop = true;
        public delegate void SelectedItemChagedEventHandler(string item);
        public event SelectedItemChagedEventHandler SelectedItemChagedEvent;
        public delegate void ItemAddedByDropEventHandler(List<string> items);
        public event ItemAddedByDropEventHandler ItemAddedByDropEvent;
        private List<string> m_Items = new List<string>();
        public SmartListBox()
        {
            InitializeComponent();
        }

        public void ClearList()
        {
            if (m_Items != null)
            {
                m_Items.Clear();
            }

            try
            {
                lbMain.Items.Clear();
            }
            catch { }
        }

        public void SetList(List<string> items)
        {
            m_Items.AddRange(items);
            UpdateListBoxMain();

            if (ItemAddedByDropEvent != null)
            {
                ItemAddedByDropEvent(m_Items);
            }
        }

        public List<string> GetList()
        {
            return m_Items;
        }

        public void EnableDrop(bool bEnable)
        {
            AvailableDrop = bEnable;
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItemChagedEvent != null)
            {
                string item = "";

                if (m_Items != null)
                {
                    if (m_Items.Count > 0 && m_Items.Count > lbMain.SelectedIndex && lbMain.SelectedIndex > -1)
                    {
                        item = m_Items[lbMain.SelectedIndex];
                    }
                }

                SelectedItemChagedEvent(item);
            }
        }

        private void lbMain_Drop(object sender, DragEventArgs e)
        {
            if (AvailableDrop)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string str in file)
                    {
                        lbMain.Items.Add(str);
                        m_Items.Add(str);
                    }

                    if (ItemAddedByDropEvent != null)
                    {
                        ItemAddedByDropEvent(m_Items);
                    }
                }
            }
        }

        private void lbMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (lbMain.SelectedIndex > -1)
                {
                    if (m_Items.Count > lbMain.SelectedIndex)
                    {
                        m_Items.RemoveAt(lbMain.SelectedIndex);
                        UpdateListBoxMain();

                        if (ItemAddedByDropEvent != null)
                        {
                            ItemAddedByDropEvent(m_Items);
                        }
                    }
                }
            }
        }

        private void UpdateListBoxMain()
        {
            lbMain.Items.Clear();
            foreach (string item in m_Items)
            {
                lbMain.Items.Add(item);
            }
        }
    }
}
