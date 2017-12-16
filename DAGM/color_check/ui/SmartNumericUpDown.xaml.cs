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
    /// Interaction logic for SmartNumericUpDown.xaml
    /// </summary>
    public partial class SmartNumericUpDown : UserControl
    {
        public delegate void ValueChangedEventHandler(decimal value);
        public event ValueChangedEventHandler ValueChangedEvent;
        private bool bSet = false;
        public decimal Decimalplaces
        {
            get
            {
                return m_Decimalplaces;
            }
            set
            {
                m_Decimalplaces = value;
                UpdateValue();
            }
        }
        public decimal Increment
        {
            get
            {
                return m_Increment;
            }
            set
            {
                value = m_Increment;
            }
        }
        public decimal MaxValue
        {
            get
            {
                return m_MaxValue;
            }
            set
            {
                m_MaxValue = value;
                UpdateValue();
            }
        }
        public decimal MinValue
        {
            get
            {
                return m_MinValue;
            }
            set
            {
                m_MinValue = value;
                UpdateValue();
            }
        }
        public decimal Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (value > m_MaxValue)
                {
                    m_Value = m_MaxValue;
                }
                else if (value < m_MinValue)
                {
                    m_Value = m_MinValue;
                }
                else
                {
                    m_Value = value;
                }

                UpdateValue();
            }
        }

        private decimal m_Decimalplaces = 0;
        private int m_Increment = 1;
        private decimal m_MaxValue = 100;
        private decimal m_MinValue = 0;
        private decimal m_Value = 0;
        public SmartNumericUpDown()
        {
            InitializeComponent();
        }

        private void UpdateValue()
        {
            tbNumeric.Text = m_Value.ToString();
        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (m_Value - m_Increment >= m_MinValue)
            {
                m_Value -= m_Increment;
                UpdateValue();
                if (ValueChangedEvent != null)
                {
                    ValueChangedEvent(m_Value);
                }
            }
        }

        private void btnIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (m_Value + m_Increment <= m_MaxValue)
            {
                m_Value += m_Increment;
                UpdateValue();
                if (ValueChangedEvent != null)
                {
                    ValueChangedEvent(m_Value);
                }
            }
        }

        private void tbNumeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Decimal temp = Convert.ToDecimal(tbNumeric.Text);
                if (temp >= m_MinValue && temp <= m_MaxValue)
                {
                    m_Value = temp;
                    if (ValueChangedEvent != null)
                    {
                        ValueChangedEvent(m_Value);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
