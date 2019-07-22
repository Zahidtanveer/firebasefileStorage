Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports Firebase.Storage

Partial Class _Default

    Inherits Page
    Sub Page_Load(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Do stuff
        'Get All Image Urls
        Dim imageFilePath As String = Path.Combine(Server.MapPath("/Content"), "images.txt")
        Dim ImagefileUrlList = ReadUrls(imageFilePath)
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow
        dt.Columns.Add("Image", GetType(String))
        For Each a In ImagefileUrlList
            dr = dt.NewRow()
            dr.SetField(Of String)("Image", a)
            dt.Rows.Add(dr)
        Next

        GridView1.DataSource = dt
        GridView1.DataBind()


        'Get All Video Urls
        Dim videoFilePath As String = Path.Combine(Server.MapPath("/Content"), "videos.txt")
        Dim videofileUrlList = ReadUrls(videoFilePath)
        Dim dtable As DataTable = New DataTable()
        Dim drow As DataRow
        dtable.Columns.Add("Video", GetType(String))
        For Each v In videofileUrlList
            drow = dtable.NewRow()
            drow.SetField(Of String)("Video", v)
            dtable.Rows.Add(drow)
        Next
        GridView2.DataSource = dtable
        GridView2.DataBind()
    End Sub

    Protected Sub UploadVideobtn_Click(sender As Object, e As EventArgs) Handles fileUploadbtn.Click
        If fileUploadControl.HasFile Then
            Try
                If fileUploadControl.PostedFile.ContentType.Contains("video") OrElse fileUploadControl.PostedFile.ContentType.Contains("image") Then
                    Dim filename As String = Path.GetFileName(fileUploadControl.FileName)
                    Dim localPath As String = Path.Combine(Server.MapPath("/Content"), filename)
                    Dim stream As FileStream
                    Dim fileHelper As FilesCompress = New FilesCompress()
                    fileUploadControl.SaveAs(Path.Combine(localPath))

                    'Image Upload
                    If fileUploadControl.PostedFile.ContentType.Contains("image") Then

                        Dim newPath As String = fileHelper.resizeImageAndSave(localPath)
                        stream = File.Open(newPath, FileMode.Open)
                        Dim task = New FirebaseStorage("firbasefiles.appspot.com").Child("files").Child(filename).PutAsync(stream)
                        AddHandler task.Progress.ProgressChanged, Sub(s, e1)
                                                                      fileUploadStatus.Text = "Progress:" + e1.Percentage.ToString() + "%"
                                                                      fileUploadStatus.ForeColor = Color.Green
                                                                  End Sub
                        Dim downloadUrl = task.GetAwaiter().GetResult()
                        fileUploadStatus.Text = "File uploaded Success..! " + downloadUrl
                        fileUploadStatus.ForeColor = Color.Green
                        Dim ImagesPath As String = Path.Combine(Server.MapPath("/Content"), "images.txt")
                        LogWrite(downloadUrl, ImagesPath)
                        If File.Exists(newPath) Then
                            File.Delete(newPath)
                        End If
                    End If

                    'Video Upload
                    If fileUploadControl.PostedFile.ContentType.Contains("video") Then
                        stream = File.Open(localPath, FileMode.Open)
                        Dim task = New FirebaseStorage("firbasefiles.appspot.com").Child("files").Child(filename).PutAsync(stream)
                        AddHandler task.Progress.ProgressChanged, Sub(s, e1)
                                                                      fileUploadStatus.Text = "Progress:" + e1.Percentage.ToString() + "%"
                                                                      fileUploadStatus.ForeColor = Color.Green
                                                                  End Sub
                        Dim downloadUrl = task.GetAwaiter().GetResult()
                        fileUploadStatus.Text = "File uploaded Success..! " + downloadUrl
                        fileUploadStatus.ForeColor = Color.Green
                        Dim VideosPath As String = Path.Combine(Server.MapPath("/Content"), "videos.txt")
                        LogWrite(downloadUrl, VideosPath)
                    End If

                    If File.Exists(localPath) Then
                        File.Delete(localPath)
                    End If



                Else
                    fileUploadStatus.Text = "Only Image & Video files are accepted..!"
                    fileUploadStatus.ForeColor = Color.Red
                End If
            Catch ex As Exception
                fileUploadStatus.Text = "File Upload Error : " & ex.Message
                fileUploadStatus.ForeColor = Color.Red
            End Try
        End If
    End Sub
End Class