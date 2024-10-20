Imports System.Collections.Generic

Public Class Form1

    ' Variabel stack untuk menyimpan nilai yang dihapus
    Private deletedValues As New Stack(Of String)

    ' Variabel list untuk menyimpan riwayat hasil kalkulasi
    Private history As New List(Of String)

    ' Event handler untuk tombol penjumlahan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            Dim result As Decimal = Val(boxNilai1.Text) + Val(boxNilai2.Text) ' Menjumlahkan nilai
            boxHasil.Text = result.ToString()
            AddToHistory($"{boxNilai1.Text} + {boxNilai2.Text} = {result}") ' Simpan ke riwayat
        End If
    End Sub

    ' Event handler yang dijalankan saat form dimuat
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Pesan ucapan selamat datang
        MessageBox.Show("Selamat datang di aplikasi kalkulator! Aplikasi ini mendukung penjumlahan, pengurangan, perkalian, pembagian, dan konversi persentase.", "Selamat Datang", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Me.KeyPreview = True ' Mengaktifkan KeyPreview untuk menangkap event KeyDown di form
        boxNilai1.Focus() ' Mengatur fokus ke boxNilai1 ketika form dijalankan

        ' Menambahkan deteksi waktu lokal untuk otomatisasi dark mode
        Dim localTime As DateTime = DateTime.Now
        Dim centralIndonesiaTime As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(localTime, "SE Asia Standard Time")

        ' Mode gelap otomatis diaktifkan antara pukul 18:00 dan 06:00
        If centralIndonesiaTime.Hour >= 18 OrElse centralIndonesiaTime.Hour < 6 Then
            ' Mengaktifkan mode gelap
            BackColor = Color.FromArgb(40, 40, 40) ' Warna abu-abu gelap
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.FromArgb(40, 40, 40) ' Ubah latar belakang kontrol menjadi abu-abu gelap
                ctrl.ForeColor = Color.White ' Ubah warna teks menjadi putih
            Next
            Button7.Text = "Light Mode" ' Ubah teks tombol
            isDarkMode = True ' Setel status mode gelap
        Else
            ' Mode terang tetap
            BackColor = Color.White ' Ubah latar belakang menjadi putih
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.White ' Ubah latar belakang kontrol menjadi putih
                ctrl.ForeColor = Color.Black ' Ubah warna teks menjadi hitam
            Next
            Button7.Text = "Dark Mode" ' Ubah teks tombol
            isDarkMode = False ' Setel status mode gelap
        End If
    End Sub

    ' Event handler untuk tombol pengurangan
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            Dim result As Decimal = Val(boxNilai1.Text) - Val(boxNilai2.Text) ' Mengurangkan nilai
            boxHasil.Text = result.ToString()
            AddToHistory($"{boxNilai1.Text} - {boxNilai2.Text} = {result}") ' Simpan ke riwayat
        End If
    End Sub

    ' Event handler untuk tombol perkalian
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            Dim result As Decimal = Val(boxNilai1.Text) * Val(boxNilai2.Text) ' Mengalikan nilai
            boxHasil.Text = result.ToString()
            AddToHistory($"{boxNilai1.Text} * {boxNilai2.Text} = {result}") ' Simpan ke riwayat
        End If
    End Sub

    ' Event handler untuk tombol pembagian
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ValidateInput() Then ' Memeriksa apakah input valid
            If Val(boxNilai2.Text) = 0 Then
                boxHasil.Text = "infinity" ' Jika nilai di boxNilai2 adalah 0, tampilkan 'infinity'
            Else
                Dim result As Decimal = Val(boxNilai1.Text) / Val(boxNilai2.Text) ' Lakukan pembagian jika tidak 0
                boxHasil.Text = result.ToString()
                AddToHistory($"{boxNilai1.Text} / {boxNilai2.Text} = {result}") ' Simpan ke riwayat
            End If
        End If
    End Sub

    ' Event handler untuk tombol hapus
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Simpan nilai yang dihapus ke stack
        deletedValues.Push(boxNilai1.Text) ' Simpan nilai pertama
        deletedValues.Push(boxNilai2.Text) ' Simpan nilai kedua

        boxNilai1.Text = "" ' Kosongkan input nilai pertama
        boxNilai2.Text = "" ' Kosongkan input nilai kedua
        boxHasil.Text = "" ' Kosongkan hasil
        boxNilai1.Focus() ' Mengembalikan fokus ke boxNilai1
    End Sub

    ' Fungsi untuk tombol persen (Button6)
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ValidateInput() Then
            Dim result As Decimal = Val(boxNilai1.Text) / 100 ' Hitung persen dari nilai pertama
            boxHasil.Text = result.ToString()
            AddToHistory($"{boxNilai1.Text} % = {result}") ' Simpan ke riwayat
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

        Return True ' Input valid
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

    ' Event handler untuk tombol enter di boxNilai1
    Private Sub boxNilai1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles boxNilai1.KeyPress
        If e.KeyChar = Chr(13) Then
            boxNilai2.Focus() ' Pindah fokus ke boxNilai2 ketika enter ditekan
        End If
    End Sub

    ' Event handler untuk tombol enter di boxNilai2
    Private Sub boxNilai2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles boxNilai2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus() ' Pindah fokus ke tombol penjumlahan ketika enter ditekan
        End If
    End Sub

    ' Variabel untuk menyimpan status mode gelap
    Dim isDarkMode As Boolean = False

    ' Event handler untuk tombol mode gelap
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Jika mode gelap aktif, kembali ke mode terang
        If isDarkMode Then
            BackColor = Color.White ' Ubah latar belakang menjadi putih
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.White ' Ubah latar belakang kontrol menjadi putih
                ctrl.ForeColor = Color.Black ' Ubah warna teks menjadi hitam
            Next
            Button7.Text = "Dark Mode" ' Ubah teks tombol
            isDarkMode = False ' Ubah status mode gelap
        Else ' Jika mode terang, ubah ke mode gelap
            BackColor = Color.FromArgb(40, 40, 40) ' Warna abu-abu gelap
            For Each ctrl As Control In Controls
                ctrl.BackColor = Color.FromArgb(40, 40, 40) ' Ubah latar belakang kontrol menjadi abu-abu gelap
                ctrl.ForeColor = Color.White ' Ubah warna teks menjadi putih
            Next
            Button7.Text = "Light Mode" ' Ubah teks tombol
            isDarkMode = True ' Ubah status mode gelap
        End If
    End Sub

    ' Event handler untuk tombol panduan
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

    ' Event handler untuk tombol keluar
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' Menampilkan kotak pesan konfirmasi sebelum keluar
        Dim result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Jika pengguna memilih Yes, maka aplikasi akan ditutup
        If result = DialogResult.Yes Then
            Application.Exit() ' Menutup aplikasi
        End If
    End Sub

    ' Tombol undo untuk mengembalikan nilai terakhir yang dihapus
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If deletedValues.Count > 0 Then
            ' Ambil nilai dari stack dan masukkan ke boxNilai1 atau boxNilai2
            boxNilai2.Text = deletedValues.Pop ' Kembalikan nilai kedua
            If deletedValues.Count > 0 Then
                boxNilai1.Text = deletedValues.Pop ' Kembalikan nilai pertama
            End If
        Else
            MessageBox.Show("Tidak ada nilai yang dapat dikembalikan!", "Undo Tidak Tersedia", MessageBoxButtons.OK, MessageBoxIcon.Warning) ' Tampilkan pesan jika tidak ada nilai yang bisa dikembalikan
        End If
    End Sub

    ' Fungsi untuk menambahkan hasil ke riwayat
    Private Sub AddToHistory(entry As String)
        history.Add(entry) ' Tambahkan entri ke riwayat
    End Sub

    ' Event handler untuk tombol History
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If history.Count = 0 Then
            MessageBox.Show("Riwayat kosong!", "History", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Menampilkan riwayat
            Dim historyMessage As String = String.Join(Environment.NewLine, history)
            MessageBox.Show(historyMessage, "Riwayat Kalkulasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Konfirmasi penghapusan riwayat
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus riwayat?", "Konfirmasi Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                ' Menghapus riwayat jika pengguna mengklik "Yes"
                history.Clear()
                MessageBox.Show("Riwayat telah dihapus.", "History", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' Tidak melakukan apa-apa jika pengguna mengklik "No"
                MessageBox.Show("Riwayat tidak dihapus.", "History", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

End Class