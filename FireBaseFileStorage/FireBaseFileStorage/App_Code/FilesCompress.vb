Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports Firebase.Storage
Imports Microsoft.VisualBasic

Public Class FilesCompress
    Private imgSource As System.Drawing.Image
    Private imgOutput As System.Drawing.Image

    'Upload Files on Firebase 
    Public Function UploadonFileOnFirebaseAsync(ByVal localPath As String, ByVal fileName As String) As String

        Dim stream = File.Open(localPath, FileMode.Open)
        Dim task = New FirebaseStorage("firbasefiles.appspot.com").Child("files").Child(fileName).PutAsync(stream)
        AddHandler task.Progress.ProgressChanged, Sub(s, e)
                                                      Debug.WriteLine($"Progress: {e.Percentage} %")
                                                  End Sub
        Dim downloadUrl As String = task.GetAwaiter().GetResult()
        Return downloadUrl
    End Function

    'Compress Image File 
    Public Function resizeImageAndSave(ByVal imagePath As String) As String
        Dim fullSizeImg As System.Drawing.Image = System.Drawing.Image.FromFile(imagePath)
        Dim thumbnailImg = New Bitmap(565, 290)
        Dim thumbGraph = Graphics.FromImage(thumbnailImg)
        thumbGraph.CompositingQuality = CompositingQuality.HighQuality
        thumbGraph.SmoothingMode = SmoothingMode.HighQuality
        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic
        Dim imageRectangle = New Rectangle(0, 0, 565, 290)
        thumbGraph.DrawImage(fullSizeImg, imageRectangle)
        Dim targetPath As String = imagePath.Replace(Path.GetFileNameWithoutExtension(imagePath), Path.GetFileNameWithoutExtension(imagePath) & "-resize")
        thumbnailImg.Save(targetPath, ImageFormat.Jpeg)
        thumbnailImg.Dispose()
        fullSizeImg.Dispose()
        Return targetPath
    End Function

    'Compress VideoFile

End Class
