
using Azure.AI.FormRecognizer.DocumentAnalysis;

using FileUploadReader.Interface;
using FileUploadReader.ViewModels;

using System.Reflection.Metadata;
using System.Security.Cryptography;

public class SdkInvoiceAdapter : IInvoiceAdapter
{
  private readonly AnalyzedDocument _document;

  public SdkInvoiceAdapter(AnalyzedDocument document)
  {
    _document = document;
  }

  public List<InvoiceField> ExtractFields()
  {
    var fields = new List<InvoiceField>();

    if (_document.Fields.TryGetValue("VendorName", out var vendor) && vendor != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Vendor Name",
        Value = vendor.Content?.ToString(),
        Confidence = vendor.Confidence
      });
    }

    if (_document.Fields.TryGetValue("VendorAddress", out var vendorAddressField) && vendorAddressField != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Vendor Address",
        Value = vendorAddressField.Content,
        Confidence = vendorAddressField.Confidence
      });
    }

    //if (_document.Fields.TryGetValue("VendorAddressRecipient", out var vendorAddressRec) &&
    //         vendorAddressRec != null)
    //{
    //  fields.Add(new InvoiceField
    //  {
    //    Key = "Vendor Address Recipient",
    //    Value = vendorAddressRec.Content,
    //    Confidence = vendorAddressRec.Confidence
    //  });
    //}

    if (_document.Fields.TryGetValue("VendorPhoneNumber", out var vendorPhone) &&
           vendorPhone != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Vendor Phone Number",
        Value = vendorPhone.Content,
        Confidence = vendorPhone.Confidence
      });
    }

    if (_document.Fields.TryGetValue("VendorEmail", out var vendorEmail) &&
           vendorEmail != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Vendor Email",
        Value = vendorEmail.Content,
        Confidence = vendorEmail.Confidence
      });
    }

    if (_document.Fields.TryGetValue("CustomerName", out var cusName) &&
          cusName != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Customer Name",
        Value = cusName.Content,
        Confidence = cusName.Confidence
      });
    }

    if (_document.Fields.TryGetValue("CustomerAddress", out var cusAddress) &&
           cusAddress != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Customer Address",
        Value = cusAddress.Content,
        Confidence = cusAddress.Confidence
      });
    }

    if (_document.Fields.TryGetValue("CustomerAddressRecipient", out var cusAddRec) &&
          cusAddRec != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Customer Address Recipient",
        Value = cusAddRec.Content,
        Confidence = cusAddRec.Confidence
      });
    }

    if (_document.Fields.TryGetValue("ShippingAddress", out var shipAdd) &&
           shipAdd != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Shipping Address",
        Value = shipAdd.Content,
        Confidence = shipAdd.Confidence
      });
    }

    if (_document.Fields.TryGetValue("ShippingAddressRecipient", out var shipAddRec) &&
          shipAddRec != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Shipping Address Recipient",
        Value = shipAddRec.Content,
        Confidence = shipAddRec.Confidence
      });
    }

    if (_document.Fields.TryGetValue("BillingAddress", out var billAdd) &&
           billAdd != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Billing Address",
        Value = billAdd.Content,
        Confidence = billAdd.Confidence
      });
    }

    if (_document.Fields.TryGetValue("BillingAddressRecipient", out var billAddRec) &&
           billAddRec != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Billing Address Recipient",
        Value = billAddRec.Content,
        Confidence = billAddRec.Confidence
      });
    }

    if (_document.Fields.TryGetValue("BillingPhoneNumber", out var billPhone) &&
           billPhone != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Billing Phone Number",
        Value = billPhone.Content,
        Confidence = billPhone.Confidence
      });
    }

    if (_document.Fields.TryGetValue("BillingEmail", out var billEmail) &&
          billEmail != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Billing Email",
        Value = billEmail.Content,
        Confidence = billEmail.Confidence
      });
    }

    if (_document.Fields.TryGetValue("CustomerTaxId", out var cusTex) &&
           cusTex != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Customer Tax Id",
        Value = cusTex.Content,
        Confidence = cusTex.Confidence
      });
    }

    if (_document.Fields.TryGetValue("InvoiceDate", out var invDate) &&
           invDate != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Invoice Date",
        Value = invDate.Content,
        Confidence = invDate.Confidence
      });
    }

    if (_document.Fields.TryGetValue("InvoiceId", out var invId) &&
           invId != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Invoice Id",
        Value = invId.Content,
        Confidence = invId.Confidence
      });
    }

    if (_document.Fields.TryGetValue("PurchaseOrder", out var purOrder) &&
           purOrder != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Purchase Order",
        Value = purOrder.Content,
        Confidence = purOrder.Confidence
      });
    }

    if (_document.Fields.TryGetValue("AmountDue", out var amtDue) &&
          amtDue != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Amount Due",
        Value = amtDue.Content,
        Confidence = amtDue.Confidence
      });
    }

    if (_document.Fields.TryGetValue("SubTotal", out var subTotal) &&
          subTotal != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Sub Total",
        Value = subTotal.Content,
        Confidence = subTotal.Confidence
      });
    }
    if (_document.Fields.TryGetValue("TotalTax", out var totalTax) &&
           totalTax != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Total Tax",
        Value = totalTax.Content,
        Confidence = totalTax.Confidence
      });
    }

    if (_document.Fields.TryGetValue("InvoiceTotal", out var invTot) &&
          invTot != null)
    {
      fields.Add(new InvoiceField
      {
        Key = "Invoice Total",
        Value = invTot.Content,
        Confidence = invTot.Confidence
      });
    }

    if (_document.Fields.TryGetValue("Items", out DocumentField? itemsField) &&
    itemsField.FieldType == DocumentFieldType.List)
    {
      int count = 1;

      foreach (DocumentField itemField in itemsField.Value.AsList())
      {
        if (itemField.FieldType == DocumentFieldType.Dictionary)
        {
          var itemDict = itemField.Value.AsDictionary();

          string desc = itemDict.TryGetValue("Description", out var descField) ? descField?.Content ?? "" : "N/A";
          string qty = itemDict.TryGetValue("Quantity", out var qtyField) ? qtyField?.Content ?? "" : "N/A";
          string unit = itemDict.TryGetValue("UnitPrice", out var unitField) ? unitField?.Content ?? "" : "N/A";
          string amt = itemDict.TryGetValue("Amount", out var amtField) ? amtField?.Content ?? "" : "N/A";

          // Calculate average confidence (or min if you prefer)
          List<float?> confidences = new()
            {
                descField?.Confidence,
                qtyField?.Confidence,
                unitField?.Confidence,
                amtField?.Confidence
            };

          float? avgConfidence = confidences
              .Where(c => c.HasValue)
              .Select(c => c.Value)
              .DefaultIfEmpty(0)
              .Average();

          fields.Add(new InvoiceField
          {
            Key = $"Item {count}",
            Value = $"Description: {desc}, Quantity: {qty}, Unit Price: {unit}, Amount: {amt}",
            Confidence = avgConfidence
          });

          count++;
        }
      }
    }


    return fields;
  }
}
