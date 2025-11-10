using LigthStringEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSEGui
{
    public partial class LSEGUI : Form
    {
        DatTL Editor;
        string[] Strings;
        int malieLabelCount = 0;  // MALIE LABEL 원본 개수
        int filteredMalieLabelCount = 0;  // 필터링된 MALIE LABEL 개수 (GUI 표시용)
        string currentFilePath = "";  // ✅ 현재 로드된 파일 경로

        public LSEGUI()
        {
            InitializeComponent();
            
            // ✅ Main.cs의 로그 이벤트 구독
            LigthStringEditor.LightDat.OnLog += Log;
            
            // ✅ 체크박스 이벤트 연결
            chkFilter.CheckedChanged += chkFilter_CheckedChanged;
            
            MessageBox.Show("LightStringEditor with Tab UI\n\nMALIE LABEL과 STRING TABLE이 분리되었습니다.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ GUI 로그 출력 메서드
        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => Log(message)));
            }
            else
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
        }

        // ✅ 필터 체크박스 이벤트 핸들러
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            // 파일이 로드되어 있을 때만 새로고침
            if (Editor != null && !string.IsNullOrEmpty(currentFilePath))
            {
                Log($"필터 {(chkFilter.Checked ? "활성화" : "비활성화")} - 파일 새로고침 중...");
                LoadFile(currentFilePath);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filed = new OpenFileDialog();
            filed.Filter = "EXEC Files (*.dat;*.bin)|*.dat;*.bin|All Files (*.*)|*.*";
            filed.Title = "Open EXEC File";

            if (filed.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = filed.FileName;  // ✅ 파일 경로 저장
                LoadFile(currentFilePath);
            }
        }

        // ✅ 파일 로드 로직을 별도 메서드로 분리
        private void LoadFile(string filePath)
        {
            byte[] File = System.IO.File.ReadAllBytes(filePath);
            Editor = new DatTL(File);
            
            // ✅ Import 전에 필터 상태 적용
            Editor.FilterEnabled = chkFilter.Checked;

            Strings = Editor.Import();

            // ✅ MALIE LABEL 개수 가져오기
            malieLabelCount = Editor.MalieLabelCount;  // 원본 개수
            filteredMalieLabelCount = Editor.FilteredMalieLabelCount;  // 필터링된 개수 (GUI 표시용)

            // ✅ Tab 1: MALIE LABEL만 표시 (필터링된 개수 사용)
            listBox1.Items.Clear();
            if (filteredMalieLabelCount > 0)
            {
                string[] malieLabels = Strings.Take(filteredMalieLabelCount).ToArray();
                listBox1.Items.AddRange(malieLabels);
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
            }

            // ✅ Tab 2: STRING TABLE만 표시 (필터링된 개수 사용)
            listBox2.Items.Clear();
            if (Strings.Length > filteredMalieLabelCount)
            {
                string[] stringTable = Strings.Skip(filteredMalieLabelCount).ToArray();
                listBox2.Items.AddRange(stringTable);
                if (listBox2.Items.Count > 0)
                    listBox2.SelectedIndex = 0;
            }

            // 파일명을 타이틀바에 표시
            string filterStatus = chkFilter.Checked ? " (필터 ON)" : " (필터 OFF)";
            this.Text = $"LightStringEditor - {Path.GetFileName(filePath)} (MALIE: {filteredMalieLabelCount}/{malieLabelCount}, STRINGS: {Strings.Length - filteredMalieLabelCount}){filterStatus}";
            
            Log($"파일 로드 완료: {Path.GetFileName(filePath)}");
        }

        // ========== MALIE LABEL (Tab 1) ==========
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = listBox1.SelectedIndex;
                if (i >= 0 && i < listBox1.Items.Count)
                {
                    textBox1.Text = listBox1.Items[i].ToString();
                    this.Text = $"MALIE LABEL - ID: {i}/{listBox1.Items.Count}";
                }
            }
            catch { }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                try
                {
                    int selectedIndex = listBox1.SelectedIndex;
                    if (selectedIndex >= 0 && selectedIndex < listBox1.Items.Count)
                    {
                        // ✅ MALIE LABEL 업데이트
                        listBox1.Items[selectedIndex] = textBox1.Text;
                        Strings[selectedIndex] = textBox1.Text;  // 원본 배열도 업데이트
                    }
                }
                catch { }
            }
        }

        // ========== STRING TABLE (Tab 2) ==========
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = listBox2.SelectedIndex;
                if (i >= 0 && i < listBox2.Items.Count)
                {
                    textBox2.Text = listBox2.Items[i].ToString();
                    this.Text = $"STRING TABLE - ID: {i}/{listBox2.Items.Count}";
                }
            }
            catch { }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                try
                {
                    int selectedIndex = listBox2.SelectedIndex;
                    if (selectedIndex >= 0 && selectedIndex < listBox2.Items.Count)
                    {
                        // ✅ STRING TABLE 업데이트 (filteredMalieLabelCount 사용)
                        listBox2.Items[selectedIndex] = textBox2.Text;
                        Strings[filteredMalieLabelCount + selectedIndex] = textBox2.Text;  // 원본 배열도 업데이트
                    }
                }
                catch { }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog filed = new SaveFileDialog();
            filed.Filter = "EXEC Files (*.dat;*.bin)|*.dat;*.bin|All Files (*.*)|*.*";
            filed.Title = "Save EXEC File";
            filed.DefaultExt = "dat";
            filed.AddExtension = true;

            if (filed.ShowDialog() == DialogResult.OK)
            {
                // ✅ 두 탭의 데이터를 하나로 합치기
                List<string> finalStrings = new List<string>();

                // MALIE LABEL
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    finalStrings.Add(listBox1.Items[i].ToString());
                }

                // STRING TABLE
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    finalStrings.Add(listBox2.Items[i].ToString());
                }

                Strings = finalStrings.ToArray();

                byte[] Script = Editor.Export(Strings);
                System.IO.File.WriteAllBytes(filed.FileName, Script);
                MessageBox.Show($"File Saved: {Path.GetFileName(filed.FileName)}\n\nMALIE LABEL: {listBox1.Items.Count}\nSTRING TABLE: {listBox2.Items.Count}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}