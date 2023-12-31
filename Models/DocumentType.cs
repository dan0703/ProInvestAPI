using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class DocumentType
{
    public int IdDocumentType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
