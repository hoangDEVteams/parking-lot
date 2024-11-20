using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Views
{
    public partial class FFormThueXe : FMain
    {
        public FFormThueXe() : base("Guest")
        {
            InitializeComponent();        }

        public FFormThueXe(string username) : base(username)
        {
            InitializeComponent();
        }
    }
    }
