using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Enums
{
    public enum EInvoiceStatus
    {
        [Description("Draft")]
        Draft = 1,

        [Description("Issued")]
        Issued = 2,

        [Description("Cancelled")]
        Cancelled = 3,

        [Description("Adjusted")]
        Adjusted = 4,

        [Description("Replaced")]
        Replaced = 5,

        [Description("Pending Approval")]
        PendingApproval = 6,

        [Description("Pending Sign")]
        PendingSign = 7,

        [Description("Signed")]
        Signed = 8,

        [Description("Sent")]
        Sent = 9,

        [Description("Adjustment In Process")]
        AdjustmentInProcess = 10,

        [Description("Replacement In Process")]
        ReplacementInProcess = 11,

        [Description("Tax Authority Approved")]
        TaxAuthorityApproved = 12,

        [Description("Tax Authority Rejected")]
        TaxAuthorityRejected = 13,

        [Description("Processing")]
        Processing = 14,

        [Description("Send Error")]
        SendError = 15,

        [Description("Rejected")]
        Rejected = 16
    }
}
