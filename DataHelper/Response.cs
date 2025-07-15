namespace FileUploadReader.DataHelper
{
  public class Response<T>
  {
    public T? Data { get; set; }

    public bool Result { get; set; }

    public string Message { get; set; } = "";
  }
}
