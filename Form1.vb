﻿Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            boxHasil.Text = Val(boxNilai1.Text) + Val(boxNilai2.Text)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True ' Mengaktifkan KeyPreview untuk menangkap event KeyDown di form
        boxNilai1.Focus() ' Mengatur fokus ke boxNilai1 ketika form dijalankan
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            boxHasil.Text = Val(boxNilai1.Text) - Val(boxNilai2.Text)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            boxHasil.Text = Val(boxNilai1.Text) * Val(boxNilai2.Text)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            If Val(boxNilai2.Text) = 0 Then
                boxHasil.Text = "infinity" ' Jika nilai di boxNilai2 adalah 0, tampilkan 'infinity'
            Else
                boxHasil.Text = Val(boxNilai1.Text) / Val(boxNilai2.Text) ' Lakukan pembagian jika tidak 0
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        boxNilai1.Text = ""
        boxNilai2.Text = ""
        boxHasil.Text = ""
        boxNilai1.Focus() ' Mengembalikan fokus ke boxNilai1
    End Sub

    ' Fungsi untuk tombol persen (Button6)
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ValidateInput() Then
            boxHasil.Text = Val(boxNilai1.Text) / 100 ' Hitung persen dari nilai pertama
        End If
    End Sub

    ' Validasi input untuk memastikan tidak ada kolom yang kosong dan hanya angka desimal yang valid
    Private Function ValidateInput() As Boolean
        ' Cek apakah ada kolom yang kosong
        If String.IsNullOrWhiteSpace(boxNilai1.Text) Then
            MessageBox.Show("Kolom Nilai 1 tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            boxNilai1.Focus() ' Mengatur fokus kembali ke boxNilai1
            Return False
        ElseIf String.IsNullOrWhiteSpace(boxNilai2.Text) Then
            MessageBox.Show("Kolom Nilai 2 tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            boxNilai2.Focus() ' Mengatur fokus kembali ke boxNilai2
            Return False
        End If

        Dim value1 As Decimal
        Dim value2 As Decimal

        ' Cek apakah boxNilai1 dan boxNilai2 berisi angka desimal
        If Not Decimal.TryParse(boxNilai1.Text, value1) Or Not Decimal.TryParse(boxNilai2.Text, value2) Then
            MessageBox.Show("Masukkan hanya angka desimal yang valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True
    End Function

    ' Menangani event KeyDown untuk menambahkan shortcut keyboard
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Menangani shortcut keyboard
        If e.Control AndAlso e.KeyCode = Keys.A Then
            e.SuppressKeyPress = True ' Mencegah default behavior (Select All)
            Button1.PerformClick() ' Ctrl + A untuk penambahan
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            e.SuppressKeyPress = True ' Mencegah default behavior
            Button2.PerformClick() ' Ctrl + S untuk pengurangan
        ElseIf e.Control AndAlso e.KeyCode = Keys.M Then
            e.SuppressKeyPress = True ' Mencegah default behavior
            Button3.PerformClick() ' Ctrl + M untuk perkalian
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            e.SuppressKeyPress = True ' Mencegah default behavior
            Button4.PerformClick() ' Ctrl + D untuk pembagian
        ElseIf e.Control AndAlso e.KeyCode = Keys.C Then
            e.SuppressKeyPress = True ' Mencegah default behavior
            Button5.PerformClick() ' Ctrl + C untuk membersihkan input
        End If
    End Sub

    ' Tambahkan KeyUp untuk tombol navigasi antar tombol
    Private Async Sub Button1_KeyUp(sender As Object, e As KeyEventArgs) Handles Button1.KeyUp
        If e.KeyCode = Keys.Right Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button2
            Button2.Focus() ' Pindah ke Button2 ketika tombol panah kanan ditekan
        End If
    End Sub

    Private Async Sub Button2_KeyUp(sender As Object, e As KeyEventArgs) Handles Button2.KeyUp
        If e.KeyCode = Keys.Right Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button3
            Button3.Focus() ' Pindah ke Button3 ketika tombol panah kanan ditekan
        ElseIf e.KeyCode = Keys.Left Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button1
            Button1.Focus() ' Pindah ke Button1 ketika tombol panah kiri ditekan
        End If
    End Sub

    Private Async Sub Button3_KeyUp(sender As Object, e As KeyEventArgs) Handles Button3.KeyUp
        If e.KeyCode = Keys.Right Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button4
            Button4.Focus() ' Pindah ke Button4 ketika tombol panah kanan ditekan
        ElseIf e.KeyCode = Keys.Left Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button2
            Button2.Focus() ' Pindah ke Button2 ketika tombol panah kiri ditekan
        End If
    End Sub

    Private Async Sub Button4_KeyUp(sender As Object, e As KeyEventArgs) Handles Button4.KeyUp
        If e.KeyCode = Keys.Right Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button5
            Button5.Focus() ' Pindah ke Button5 ketika tombol panah kanan ditekan
        ElseIf e.KeyCode = Keys.Left Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button3
            Button3.Focus() ' Pindah ke Button3 ketika tombol panah kiri ditekan
        End If
    End Sub


    Private Async Sub Button5_KeyUp(sender As Object, e As KeyEventArgs) Handles Button5.KeyUp
        If e.KeyCode = Keys.Left Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button4
            Button4.Focus() ' Pindah ke Button4 ketika tombol panah kiri ditekan
        End If
    End Sub

    ' Tambahkan navigasi tombol menggunakan KeyUp
    Private Async Sub Button6_KeyUp(sender As Object, e As KeyEventArgs) Handles Button6.KeyUp
        If e.KeyCode = Keys.Left Then
            Await Task.Delay(300) ' Menambahkan jeda 300 ms sebelum berpindah ke Button5
            Button5.Focus() ' Pindah ke Button5 ketika tombol panah kiri ditekan
        End If
    End Sub

    Private Sub boxNilai1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles boxNilai1.KeyPress
        If e.KeyChar = Chr(13) Then
            boxNilai2.Focus()
        End If
    End Sub

    Private Sub boxNilai2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles boxNilai2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus()
        End If
    End Sub

    ' Variabel untuk menyimpan status mode gelap
    Dim isDarkMode As Boolean = False

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Jika mode gelap aktif, kembali ke mode terang
        If isDarkMode Then
            BackColor = Color.White
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.White
                ctrl.ForeColor = Color.Black
            Next
            Button7.Text = "Dark Mode"
            isDarkMode = False
        Else ' Jika mode terang, ubah ke mode gelap
            BackColor = Color.FromArgb(40, 40, 40) ' Warna abu-abu gelap
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.FromArgb(40, 40, 40)
                ctrl.ForeColor = Color.White
            Next
            Button7.Text = "Light Mode"
            isDarkMode = True
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MessageBox.Show("Cara Menggunakan Aplikasi Kalkulator:\n\n" &
                        "1. Masukkan angka pertama di kolom Nilai1.\n" &
                        "2. Masukkan angka kedua di kolom Nilai2.\n" &
                        "3. Pilih operasi yang diinginkan (+, -, *, /, %).\n" &
                        "4. Hasil perhitungan akan ditampilkan di kolom Hasil.\n" &
                        "5. Klik tombol 'c' untuk menghapus input.\n" &
                        "6. Klik tombol 'Dark Mode' untuk mengubah tema aplikasi.\n\n" &
                        "Shortcut:\n" &
                        "Ctrl + A: Tambah\n" &
                        "Ctrl + S: Kurang\n" &
                        "Ctrl + M: Kali\n" &
                        "Ctrl + D: Bagi\n" &
                        "Ctrl + C: Bersihkan",
                        "Panduan Penggunaan Aplikasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' Menampilkan kotak pesan konfirmasi sebelum keluar
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Jika pengguna memilih Yes, maka aplikasi akan ditutup
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
