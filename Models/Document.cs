using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class Document
{
    public int IdDocuments { get; set; }

    public int InvestmentRequestId { get; set; }

    public int DocumentTypeId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileFormat { get; set; } = null!;

    public string FileSize { get; set; } = null!;

    public byte[] File { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual InvestmentRequest InvestmentRequest { get; set; } = null!;
}
