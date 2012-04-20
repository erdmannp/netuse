/*
This file is part of NetUse.

NetUse is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

NetUse is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with NetUse.  If not, see <http://www.gnu.org/licenses/>.

Diese Datei ist Teil von NetUse.

NetUse ist Freie Software: Sie können es unter den Bedingungen
der GNU General Public License, wie von der Free Software Foundation,
Version 3 der Lizenz oder (nach Ihrer Option) jeder späteren
veröffentlichten Version, weiterverbreiten und/oder modifizieren.

NetUse wird in der Hoffnung, dass es nützlich sein wird, aber
OHNE JEDE GEWÄHELEISTUNG, bereitgestellt; sogar ohne die implizite
Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
Siehe die GNU General Public License für weitere Details.

Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        String unc = "";

        public Form1()
        {
            InitializeComponent();

            using (StreamReader fs = new StreamReader("unc.txt"))
            {
                this.unc = fs.ReadLine();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String homeDir = string.Format("{0}\\{1}", this.unc, textBox1.Text);
            String groupDir = string.Format("{0}\\groupShares", this.unc);
            this.MapNetworkDriveConnect("Z:",  homeDir, textBox1.Text, textBox2.Text);

            this.MapNetworkDriveConnect("X:", groupDir, textBox1.Text, textBox2.Text);
            this.Close();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.MapNetworkDriveDisconnect("Z:");
            this.Close();
        }
        /// <summary>
        /// Disconnects a network drive
        /// </summary>
        /// <param name="drive">Drive (z.B. L:)</param>
        private void MapNetworkDriveDisconnect(string drive)
        {
            Process p = new Process();
            p.StartInfo.FileName = "net";
            p.StartInfo.Arguments = string.Format("use {0} /DELETE", drive);
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }
        /// <summary>
        /// Connects a network drive
        /// </summary>
        /// <param name="drive">The drive letter (e.g. L:)</param>
        /// <param name="server">The UNC path to the remote drive (e.g. \\MyServer\MyPrinter)</param>
        /// <param name="user">The User</param>
        /// <param name="password">The Password Used For Login</param>
        private void MapNetworkDriveConnect(string drive, string server, string user, string password)
        {
            Process p = new Process();
            p.StartInfo.FileName = "net";
            p.StartInfo.Arguments = string.Format("use {0} {1} /user:{2} {3}", drive, server, user, password);
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }
    }
}
