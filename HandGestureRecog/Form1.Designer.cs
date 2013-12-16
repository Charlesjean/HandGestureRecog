namespace HandGestureRecog
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxSubstraction = new System.Windows.Forms.PictureBox();
            this.pictureBoxSegmentation = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxBackGround = new System.Windows.Forms.PictureBox();
            this.pictureBoxFinalSegmentation = new System.Windows.Forms.PictureBox();
            this.pictureBoxThreshold = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBoxGradientAndMotion = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubstraction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSegmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFinalSegmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGradientAndMotion)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "After Dialte And Erode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Webcam Image:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 282);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(394, 86);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(326, 282);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBoxSubstraction
            // 
            this.pictureBoxSubstraction.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxSubstraction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSubstraction.Location = new System.Drawing.Point(12, 444);
            this.pictureBoxSubstraction.Name = "pictureBoxSubstraction";
            this.pictureBoxSubstraction.Size = new System.Drawing.Size(320, 282);
            this.pictureBoxSubstraction.TabIndex = 1;
            this.pictureBoxSubstraction.TabStop = false;
            // 
            // pictureBoxSegmentation
            // 
            this.pictureBoxSegmentation.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxSegmentation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSegmentation.Location = new System.Drawing.Point(400, 444);
            this.pictureBoxSegmentation.Name = "pictureBoxSegmentation";
            this.pictureBoxSegmentation.Size = new System.Drawing.Size(320, 282);
            this.pictureBoxSegmentation.TabIndex = 1;
            this.pictureBoxSegmentation.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Gradient Subtraction Image:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Motion Detection Image:";
            // 
            // pictureBoxBackGround
            // 
            this.pictureBoxBackGround.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxBackGround.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBackGround.Location = new System.Drawing.Point(747, 86);
            this.pictureBoxBackGround.Name = "pictureBoxBackGround";
            this.pictureBoxBackGround.Size = new System.Drawing.Size(326, 282);
            this.pictureBoxBackGround.TabIndex = 1;
            this.pictureBoxBackGround.TabStop = false;
            // 
            // pictureBoxFinalSegmentation
            // 
            this.pictureBoxFinalSegmentation.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxFinalSegmentation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFinalSegmentation.Location = new System.Drawing.Point(1091, 86);
            this.pictureBoxFinalSegmentation.Name = "pictureBoxFinalSegmentation";
            this.pictureBoxFinalSegmentation.Size = new System.Drawing.Size(326, 282);
            this.pictureBoxFinalSegmentation.TabIndex = 1;
            this.pictureBoxFinalSegmentation.TabStop = false;
            // 
            // pictureBoxThreshold
            // 
            this.pictureBoxThreshold.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThreshold.Location = new System.Drawing.Point(747, 444);
            this.pictureBoxThreshold.Name = "pictureBoxThreshold";
            this.pictureBoxThreshold.Size = new System.Drawing.Size(326, 282);
            this.pictureBoxThreshold.TabIndex = 1;
            this.pictureBoxThreshold.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(745, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "BackGround Image：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(745, 406);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(227, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Gradient Subtraction Threshold image:";
            // 
            // pictureBoxGradientAndMotion
            // 
            this.pictureBoxGradientAndMotion.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxGradientAndMotion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGradientAndMotion.Location = new System.Drawing.Point(1091, 444);
            this.pictureBoxGradientAndMotion.Name = "pictureBoxGradientAndMotion";
            this.pictureBoxGradientAndMotion.Size = new System.Drawing.Size(326, 282);
            this.pictureBoxGradientAndMotion.TabIndex = 1;
            this.pictureBoxGradientAndMotion.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1104, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(257, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Combine Motion and Gradient Segmentation：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1089, 406);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Motion Detection Threshold image:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1447, 819);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBoxGradientAndMotion);
            this.Controls.Add(this.pictureBoxFinalSegmentation);
            this.Controls.Add(this.pictureBoxThreshold);
            this.Controls.Add(this.pictureBoxBackGround);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBoxSubstraction);
            this.Controls.Add(this.pictureBoxSegmentation);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubstraction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSegmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFinalSegmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGradientAndMotion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxSubstraction;
        private System.Windows.Forms.PictureBox pictureBoxSegmentation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBoxBackGround;
        private System.Windows.Forms.PictureBox pictureBoxFinalSegmentation;
        private System.Windows.Forms.PictureBox pictureBoxThreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxGradientAndMotion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

