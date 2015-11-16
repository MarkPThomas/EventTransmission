<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCurrentProperty = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBxNewProperty = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnChangeProperty = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblCurrentPropertyDoll = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBxNewPropertyDoll = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnChangePropertyDoll = New System.Windows.Forms.Button()
        Me.btnSwapChildReferences = New System.Windows.Forms.Button()
        Me.lblParent = New System.Windows.Forms.Label()
        Me.btnSwapParents = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblCurrentProperty)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtBxNewProperty)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnChangeProperty)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 29)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(138, 143)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Child"
        '
        'lblCurrentProperty
        '
        Me.lblCurrentProperty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentProperty.Location = New System.Drawing.Point(6, 31)
        Me.lblCurrentProperty.Name = "lblCurrentProperty"
        Me.lblCurrentProperty.Size = New System.Drawing.Size(100, 20)
        Me.lblCurrentProperty.TabIndex = 10
        Me.lblCurrentProperty.Text = "Last Property"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "New Property"
        '
        'txtBxNewProperty
        '
        Me.txtBxNewProperty.Location = New System.Drawing.Point(6, 83)
        Me.txtBxNewProperty.Name = "txtBxNewProperty"
        Me.txtBxNewProperty.Size = New System.Drawing.Size(100, 20)
        Me.txtBxNewProperty.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Current Property"
        '
        'btnChangeProperty
        '
        Me.btnChangeProperty.Location = New System.Drawing.Point(6, 109)
        Me.btnChangeProperty.Name = "btnChangeProperty"
        Me.btnChangeProperty.Size = New System.Drawing.Size(119, 23)
        Me.btnChangeProperty.TabIndex = 6
        Me.btnChangeProperty.Text = "Change Property"
        Me.btnChangeProperty.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblCurrentPropertyDoll)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtBxNewPropertyDoll)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnChangePropertyDoll)
        Me.GroupBox2.Location = New System.Drawing.Point(156, 29)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(138, 143)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Child's Doll"
        '
        'lblCurrentPropertyDoll
        '
        Me.lblCurrentPropertyDoll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentPropertyDoll.Location = New System.Drawing.Point(6, 31)
        Me.lblCurrentPropertyDoll.Name = "lblCurrentPropertyDoll"
        Me.lblCurrentPropertyDoll.Size = New System.Drawing.Size(100, 20)
        Me.lblCurrentPropertyDoll.TabIndex = 10
        Me.lblCurrentPropertyDoll.Text = "Last Property"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "New Property"
        '
        'txtBxNewPropertyDoll
        '
        Me.txtBxNewPropertyDoll.Location = New System.Drawing.Point(6, 83)
        Me.txtBxNewPropertyDoll.Name = "txtBxNewPropertyDoll"
        Me.txtBxNewPropertyDoll.Size = New System.Drawing.Size(100, 20)
        Me.txtBxNewPropertyDoll.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Current Property"
        '
        'btnChangePropertyDoll
        '
        Me.btnChangePropertyDoll.Location = New System.Drawing.Point(6, 109)
        Me.btnChangePropertyDoll.Name = "btnChangePropertyDoll"
        Me.btnChangePropertyDoll.Size = New System.Drawing.Size(119, 23)
        Me.btnChangePropertyDoll.TabIndex = 6
        Me.btnChangePropertyDoll.Text = "Change Property"
        Me.btnChangePropertyDoll.UseVisualStyleBackColor = True
        '
        'btnSwapChildReferences
        '
        Me.btnSwapChildReferences.Location = New System.Drawing.Point(86, 178)
        Me.btnSwapChildReferences.Name = "btnSwapChildReferences"
        Me.btnSwapChildReferences.Size = New System.Drawing.Size(137, 23)
        Me.btnSwapChildReferences.TabIndex = 11
        Me.btnSwapChildReferences.Text = "Swap Child References"
        Me.btnSwapChildReferences.UseVisualStyleBackColor = True
        '
        'lblParent
        '
        Me.lblParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblParent.Location = New System.Drawing.Point(96, 9)
        Me.lblParent.Name = "lblParent"
        Me.lblParent.Size = New System.Drawing.Size(119, 17)
        Me.lblParent.TabIndex = 13
        Me.lblParent.Text = "Passive Parent"
        Me.lblParent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSwapParents
        '
        Me.btnSwapParents.Location = New System.Drawing.Point(86, 207)
        Me.btnSwapParents.Name = "btnSwapParents"
        Me.btnSwapParents.Size = New System.Drawing.Size(137, 23)
        Me.btnSwapParents.TabIndex = 14
        Me.btnSwapParents.Text = "Swap Parents"
        Me.btnSwapParents.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 242)
        Me.Controls.Add(Me.btnSwapParents)
        Me.Controls.Add(Me.lblParent)
        Me.Controls.Add(Me.btnSwapChildReferences)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Event Transmission"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblCurrentProperty As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBxNewProperty As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnChangeProperty As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblCurrentPropertyDoll As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtBxNewPropertyDoll As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnChangePropertyDoll As Button
    Friend WithEvents btnSwapChildReferences As System.Windows.Forms.Button
    Friend WithEvents lblParent As System.Windows.Forms.Label
    Friend WithEvents btnSwapParents As System.Windows.Forms.Button
End Class
