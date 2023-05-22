using System.Diagnostics;
using System.Xml.Linq;

namespace AirPort3.BL {
    public class RunNodeAndReact {
        public RunNodeAndReact() {
            url = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
        }
        string url;
        public void StartProcessReact() {
            try {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "cmd.exe";
                ps.WindowStyle = ProcessWindowStyle.Normal;
                ps.WorkingDirectory = url + @"\airport-ui";
                ps.Arguments = @"/k npm start";
                Process.Start(ps);

                ProcessStartInfo ps1 = new ProcessStartInfo();
                ps1.FileName = "cmd.exe";
                ps1.WindowStyle = ProcessWindowStyle.Normal;
                ps1.WorkingDirectory = url + @"\airport-ui";
                ps1.Arguments = @"/k explorer http://localhost:3000/";
                Process.Start(ps1);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Process failed - start maniualy");
            }
        }
        public void StartProcessSimulator() {
            try {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "cmd.exe";
                ps.WindowStyle = ProcessWindowStyle.Normal;
                ps.WorkingDirectory = url + @"\node simulator";
                ps.Arguments = @"/k npm start";
                Process.Start(ps);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Process failed - start maniualy");
            }
        }
    }
}
