using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
  public interface IInvoiceAdapter
  {
    List<InvoiceField> ExtractFields();
  }
}
