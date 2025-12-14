namespace FitHubUI
{
    partial class Booking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            valgtMedlem = new CheckedListBox();
            button1 = new Button();
            textBox4 = new TextBox();
            button7 = new Button();
            textBox6 = new TextBox();
            textBox9 = new TextBox();
            InstruktørOgLokation = new ListBox();
            button2 = new Button();
            panel3 = new Panel();
            listBox1 = new ListBox();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            panel1 = new Panel();
            button3 = new Button();
            textBox2 = new TextBox();
            panel2 = new Panel();
            button4 = new Button();
            Fejl = new ListBox();
            panel4 = new Panel();
            JaGåTilbage = new Button();
            button5 = new Button();
            textBox5 = new TextBox();
            panel5 = new Panel();
            textBox12 = new TextBox();
            textBox11 = new TextBox();
            textBox8 = new TextBox();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            listBox2 = new ListBox();
            pictureBox3 = new PictureBox();
            comboBox4 = new ComboBox();
            textBox14 = new TextBox();
            button11 = new Button();
            textBox15 = new TextBox();
            comboBox5 = new ComboBox();
            textBox16 = new TextBox();
            imageList1 = new ImageList(components);
            flowLayoutPanel1 = new FlowLayoutPanel();
            button12 = new Button();
            button13 = new Button();
            textBox7 = new TextBox();
            button14 = new Button();
            button6 = new Button();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 90);
            label1.Name = "label1";
            label1.Size = new Size(130, 20);
            label1.TabIndex = 2;
            label1.Text = "Vælg Medlemmer:";
            label1.Click += label1_Click;
            // 
            // valgtMedlem
            // 
            valgtMedlem.FormattingEnabled = true;
            valgtMedlem.Location = new Point(66, 168);
            valgtMedlem.Margin = new Padding(3, 4, 3, 4);
            valgtMedlem.Name = "valgtMedlem";
            valgtMedlem.Size = new Size(205, 224);
            valgtMedlem.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(353, 418);
            button1.Name = "button1";
            button1.Size = new Size(187, 29);
            button1.TabIndex = 0;
            button1.Text = "Videre";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(39, 44);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Oversigt over Booking";
            textBox4.Size = new Size(179, 27);
            textBox4.TabIndex = 11;
            // 
            // button7
            // 
            button7.Location = new Point(512, 271);
            button7.Name = "button7";
            button7.Size = new Size(94, 29);
            button7.TabIndex = 10;
            button7.Text = "Bekræft";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(355, 161);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(150, 27);
            textBox6.TabIndex = 10;
            textBox6.Text = "Holdtype";
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(717, 35);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(125, 27);
            textBox9.TabIndex = 9;
            textBox9.Text = "Tidspunkt";
            textBox9.TextChanged += textBox9_TextChanged;
            // 
            // InstruktørOgLokation
            // 
            InstruktørOgLokation.FormattingEnabled = true;
            InstruktørOgLokation.Location = new Point(512, 145);
            InstruktørOgLokation.Name = "InstruktørOgLokation";
            InstruktørOgLokation.Size = new Size(162, 104);
            InstruktørOgLokation.TabIndex = 16;
            InstruktørOgLokation.SelectedIndexChanged += Instruktør_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(40, 110);
            button2.Name = "button2";
            button2.Size = new Size(178, 29);
            button2.TabIndex = 22;
            button2.Text = "Tilmeldte medlemmer";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(textBox7);
            panel3.Controls.Add(listBox2);
            panel3.Controls.Add(button10);
            panel3.Controls.Add(button9);
            panel3.Controls.Add(listBox1);
            panel3.Controls.Add(textBox3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(InstruktørOgLokation);
            panel3.Controls.Add(button7);
            panel3.Controls.Add(textBox4);
            panel3.Location = new Point(616, 321);
            panel3.Name = "panel3";
            panel3.Size = new Size(1249, 345);
            panel3.TabIndex = 8;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(248, 145);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(240, 104);
            listBox1.TabIndex = 29;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(248, 112);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(240, 27);
            textBox3.TabIndex = 27;
            textBox3.Text = "HoldType og tidspunkt";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(66, 123);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(205, 27);
            textBox1.TabIndex = 25;
            textBox1.Text = "Søgefelt?";
            textBox1.TextChanged += textBox1_TextChanged_2;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(textBox2);
            panel1.Location = new Point(256, 708);
            panel1.Name = "panel1";
            panel1.Size = new Size(354, 229);
            panel1.TabIndex = 26;
            // 
            // button3
            // 
            button3.Location = new Point(114, 124);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 0;
            button3.Text = "Videre";
            button3.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(94, 49);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(137, 27);
            textBox2.TabIndex = 0;
            textBox2.Text = "Booking Godkendt";
            textBox2.TextChanged += textBox2_TextChanged_1;
            // 
            // panel2
            // 
            panel2.Controls.Add(button4);
            panel2.Controls.Add(Fejl);
            panel2.Location = new Point(656, 708);
            panel2.Name = "panel2";
            panel2.Size = new Size(360, 229);
            panel2.TabIndex = 27;
            // 
            // button4
            // 
            button4.Location = new Point(127, 162);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 28;
            button4.Text = "Tilbage";
            button4.UseVisualStyleBackColor = true;
            // 
            // Fejl
            // 
            Fejl.FormattingEnabled = true;
            Fejl.Location = new Point(97, 34);
            Fejl.Name = "Fejl";
            Fejl.Size = new Size(150, 104);
            Fejl.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(JaGåTilbage);
            panel4.Controls.Add(button5);
            panel4.Controls.Add(textBox5);
            panel4.Location = new Point(1062, 708);
            panel4.Name = "panel4";
            panel4.Size = new Size(364, 229);
            panel4.TabIndex = 27;
            // 
            // JaGåTilbage
            // 
            JaGåTilbage.Location = new Point(53, 124);
            JaGåTilbage.Name = "JaGåTilbage";
            JaGåTilbage.Size = new Size(94, 29);
            JaGåTilbage.TabIndex = 2;
            JaGåTilbage.Text = "Ja";
            JaGåTilbage.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(203, 124);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 1;
            button5.Text = "Annuller";
            button5.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(66, 34);
            textBox5.Name = "textBox5";
            textBox5.PlaceholderText = "Er du sikker på du vil gå tilbage?";
            textBox5.Size = new Size(231, 27);
            textBox5.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(button12);
            panel5.Controls.Add(button13);
            panel5.Controls.Add(flowLayoutPanel1);
            panel5.Controls.Add(textBox16);
            panel5.Controls.Add(comboBox5);
            panel5.Controls.Add(textBox15);
            panel5.Controls.Add(button11);
            panel5.Controls.Add(textBox14);
            panel5.Controls.Add(comboBox4);
            panel5.Controls.Add(pictureBox3);
            panel5.Controls.Add(textBox12);
            panel5.Controls.Add(textBox8);
            panel5.Controls.Add(textBox9);
            panel5.Location = new Point(616, 12);
            panel5.Name = "panel5";
            panel5.Size = new Size(1165, 303);
            panel5.TabIndex = 28;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(570, 35);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(125, 27);
            textBox12.TabIndex = 33;
            textBox12.Text = "ledige pladser";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(354, 288);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(150, 27);
            textBox11.TabIndex = 32;
            textBox11.Text = "Favorit Hold";
            textBox11.TextChanged += textBox11_TextChanged_1;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(859, 35);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(165, 27);
            textBox8.TabIndex = 29;
            textBox8.Text = "Instruktør og Lokation";
            textBox8.TextChanged += textBox8_TextChanged_1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(354, 194);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 33;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(353, 321);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 34;
            // 
            // button8
            // 
            button8.Location = new Point(66, 466);
            button8.Name = "button8";
            button8.Size = new Size(189, 29);
            button8.TabIndex = 35;
            button8.Text = "Se Oversigt over Hold";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(38, 271);
            button9.Name = "button9";
            button9.Size = new Size(180, 29);
            button9.TabIndex = 30;
            button9.Text = "Ændre medlemmer";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click_1;
            // 
            // button10
            // 
            button10.Location = new Point(248, 271);
            button10.Name = "button10";
            button10.Size = new Size(240, 29);
            button10.TabIndex = 31;
            button10.Text = "Ændre Holdtype eller tidspunkt";
            button10.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(39, 145);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(179, 104);
            listBox2.TabIndex = 32;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(346, 111);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 62);
            pictureBox3.TabIndex = 38;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(38, 78);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(151, 28);
            comboBox4.TabIndex = 37;
            // 
            // textBox14
            // 
            textBox14.Location = new Point(38, 45);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(151, 27);
            textBox14.TabIndex = 39;
            textBox14.Text = "Måned";
            // 
            // button11
            // 
            button11.Location = new Point(38, 225);
            button11.Name = "button11";
            button11.Size = new Size(189, 29);
            button11.TabIndex = 40;
            button11.Text = "Se Oversigt over Hold";
            button11.UseVisualStyleBackColor = true;
            // 
            // textBox15
            // 
            textBox15.Location = new Point(40, 134);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(151, 27);
            textBox15.TabIndex = 41;
            textBox15.Text = "Dag";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(38, 167);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(151, 28);
            comboBox5.TabIndex = 42;
            // 
            // textBox16
            // 
            textBox16.Location = new Point(346, 75);
            textBox16.Name = "textBox16";
            textBox16.PlaceholderText = "Medlemmer";
            textBox16.Size = new Size(125, 27);
            textBox16.TabIndex = 43;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(570, 78);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(454, 206);
            flowLayoutPanel1.TabIndex = 44;
            // 
            // button12
            // 
            button12.Location = new Point(329, 255);
            button12.Name = "button12";
            button12.Size = new Size(159, 29);
            button12.TabIndex = 36;
            button12.Text = "Videre til Bekræft";
            button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Location = new Point(40, 260);
            button13.Name = "button13";
            button13.Size = new Size(94, 29);
            button13.TabIndex = 37;
            button13.Text = "Tilbage";
            button13.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(512, 112);
            textBox7.Name = "textBox7";
            textBox7.PlaceholderText = "Instruktør og lokation";
            textBox7.Size = new Size(162, 27);
            textBox7.TabIndex = 33;
            // 
            // button14
            // 
            button14.Location = new Point(66, 418);
            button14.Name = "button14";
            button14.Size = new Size(189, 29);
            button14.TabIndex = 36;
            button14.Text = "Gå til medlemsinfo";
            button14.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(66, 515);
            button6.Name = "button6";
            button6.Size = new Size(189, 29);
            button6.TabIndex = 37;
            button6.Text = "Reset valgte medlemmer";
            button6.UseVisualStyleBackColor = true;
            // 
            // Booking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1914, 1055);
            Controls.Add(button6);
            Controls.Add(button14);
            Controls.Add(button8);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(panel3);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(textBox11);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(valgtMedlem);
            Controls.Add(label1);
            Controls.Add(textBox6);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Booking";
            Text = "Booking";
            Load += Booking_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private CheckedListBox valgtMedlem;
        private Button button1;
        private TextBox textBox4;
        private Button button7;
        private TextBox textBox6;
        private TextBox textBox9;
        private ListBox InstruktørOgLokation;
        private Button button2;
        private Panel panel3;
        private TextBox textBox1;
        private Panel panel1;
        private TextBox textBox2;
        private Button button3;
        private Panel panel2;
        private Button button4;
        private ListBox Fejl;
        private Panel panel4;
        private Button JaGåTilbage;
        private Button button5;
        private TextBox textBox5;
        private Panel panel5;
        private TextBox textBox8;
        private TextBox textBox11;
        private TextBox textBox3;
        private ListBox listBox1;
        private TextBox textBox12;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Button button10;
        private Button button9;
        private Button button8;
        private ListBox listBox2;
        private PictureBox pictureBox3;
        private ComboBox comboBox4;
        private TextBox textBox14;
        private TextBox textBox15;
        private Button button11;
        private TextBox textBox16;
        private ComboBox comboBox5;
        private ImageList imageList1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button12;
        private Button button13;
        private TextBox textBox7;
        private Button button14;
        private Button button6;
    }
}