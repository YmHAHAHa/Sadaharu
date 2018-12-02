using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sadaharu.Mybuttons
{
    public partial class AdjustButton : Button
    {
        public AdjustButton() : base()
        {
            InitializeComponent();
        }

        public AdjustButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
