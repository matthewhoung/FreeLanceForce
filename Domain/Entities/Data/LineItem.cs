using System;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class LineItem
    {
        public int LineItemId { get; private set; }
        public int FormId { get; private set; }
        public List<LineItemDetail> LineItemDetails { get; private set; } = new List<LineItemDetail>();

        private LineItem()
        { 
        }

        public LineItem(int formId)
        {
            FormId = formId;
        }

        public void AddLineItemDetail(LineItemDetail lineItemDetail)
        {
            if (lineItemDetail == null) 
                throw new ArgumentNullException(nameof(lineItemDetail));

            LineItemDetails.Add(lineItemDetail);
        }

        public void ApproveDetail(int detailIndex, DateTime approvedAt)
        {
            if (detailIndex < 0 || detailIndex >= LineItemDetails.Count) 
                throw new ArgumentOutOfRangeException(nameof(detailIndex));

            var detail = LineItemDetails[detailIndex];
            detail.Approve(approvedAt);
        }

        public void RejectDetail(int detailIndex, DateTime rejectedAt)
        {
            if (detailIndex < 0 || detailIndex >= LineItemDetails.Count) 
                throw new ArgumentOutOfRangeException(nameof(detailIndex));

            var detail = LineItemDetails[detailIndex];
            detail.Reject(rejectedAt);
        }
    }
}
