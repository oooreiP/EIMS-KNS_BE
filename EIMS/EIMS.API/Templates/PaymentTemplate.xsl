<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:decimal-format name="vn" decimal-separator="," grouping-separator="." />

  <xsl:template match="/">
    <html>
      <head>
        <style>
          body { font-family: 'Times New Roman', sans-serif; font-size: 13px; }
          .header { text-align: center; margin-bottom: 20px; }
          .title { font-size: 18px; font-weight: bold; margin: 10px 0; }
          .customer-info { margin-bottom: 15px; }
          
          /* Style cho bảng dữ liệu */
          table { width: 100%; border-collapse: collapse; margin-top: 10px; }
          th, td { border: 1px solid black; padding: 5px; vertical-align: middle; }
          th { background-color: #f0f0f0; text-align: center; font-weight: bold; }
          
          /* Căn lề cho các cột số */
          .text-right { text-align: right; }
          .text-center { text-align: center; }
          .text-bold { font-weight: bold; }
          
          .footer-note { font-style: italic; margin-top: 10px; font-size: 12px; }
          .signature-section { margin-top: 30px; display: flex; justify-content: space-between; }
        </style>
      </head>
      <body>
        <div class="header">
          <div><xsl:value-of select="PaymentStatement/ProviderInfo/Name"/></div>
          <div class="title">BẢNG KÊ THANH TOÁN (PAYMENT LIST)</div>
        </div>

        <div class="customer-info">
          <div><b>Khách hàng (Customer):</b> <xsl:value-of select="PaymentStatement/HeaderInfo/CustomerName"/> (<xsl:value-of select="PaymentStatement/HeaderInfo/CustomerCode"/>)</div>
          <div><b>Tháng (Month):</b> <xsl:value-of select="PaymentStatement/HeaderInfo/StatementMonth"/></div>
        </div>

        <table>
          <thead>
            <tr>
              <th rowspan="2">Khoản mục<br/>(Description)</th>
              <th colspan="2">Thời gian (Period)</th>
              <th colspan="2">Chỉ số (Indicator)</th>
              <th rowspan="2">Khối lượng<br/>(Quantity)</th>
              <th rowspan="2">Đơn giá<br/>(Unit Price)</th>
              <th rowspan="2">Thành tiền<br/>(Amount)</th>
              <th colspan="2">Thuế GTGT (VAT)</th>
              <th rowspan="2">Tổng cộng kỳ này<br/>(Total amount)</th>
              <th rowspan="2">Tổng nợ kỳ trước<br/>(Previous liabilities)</th>
              <th rowspan="2">Tổng cộng<br/>(Total Payable)</th>
            </tr>
            <tr>
              <th>Từ (From)</th>
              <th>Đến (To)</th>
              <th>Cũ (Old)</th>
              <th>Mới (New)</th>
              <th>Thuế suất</th>
              <th>Tiền thuế</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="PaymentStatement/Items/Item">
              <tr>
                <td><xsl:value-of select="Description"/></td>
                <td class="text-center"><xsl:value-of select="PeriodFrom"/></td>
                <td class="text-center"><xsl:value-of select="PeriodTo"/></td>
                <td class="text-center"><xsl:value-of select="IndicatorOld"/></td>
                <td class="text-center"><xsl:value-of select="IndicatorNew"/></td>
                <td class="text-center"><xsl:value-of select="Quantity"/></td>
                <td class="text-right">
                  <xsl:value-of select="format-number(UnitPrice, '#.###', 'vn')"/>
                </td>
                <td class="text-right">
                  <xsl:value-of select="format-number(Amount, '#.###', 'vn')"/>
                </td>
                <td class="text-center"><xsl:value-of select="VATRate"/>%</td>
                <td class="text-right">
                  <xsl:value-of select="format-number(VATAmount, '#.###', 'vn')"/>
                </td>
                <td class="text-right">
                   <xsl:value-of select="format-number(TotalCurrent, '#.###', 'vn')"/>
                </td>
                <td class="text-right">
                  <xsl:value-of select="format-number(PreviousDebt, '#.###', 'vn')"/>
                </td>
                <td class="text-right text-bold">
                  <xsl:value-of select="format-number(TotalPayable, '#.###', 'vn')"/>
                </td>
              </tr>
            </xsl:for-each>

            <tr class="text-bold">
              <td colspan="7" class="text-center">TỔNG CỘNG (Total)</td>
              <td class="text-right">
                <xsl:value-of select="format-number(PaymentStatement/Summary/TotalAmount, '#.###', 'vn')"/>
              </td>
              <td></td>
              <td class="text-right">
                <xsl:value-of select="format-number(PaymentStatement/Summary/TotalVAT, '#.###', 'vn')"/>
              </td>
              <td class="text-right">
                <xsl:value-of select="format-number(PaymentStatement/Summary/TotalCurrentPeriod, '#.###', 'vn')"/>
              </td>
              <td class="text-right">
                <xsl:value-of select="format-number(PaymentStatement/Summary/TotalPreviousDebt, '#.###', 'vn')"/>
              </td>
              <td class="text-right">
                <xsl:value-of select="format-number(PaymentStatement/Summary/GrandTotal, '#.###', 'vn')"/>
              </td>
            </tr>
          </tbody>
        </table>

        <div class="footer-note">
          * Nợ tháng trước vẫn còn thể hiện trên bảng kê nếu khách hàng thanh toán sau ngày ra bảng kê này.<br/>
          (Liabilities from the previous period will still appear on the payment list if the customer makes a payment after the date of this list)
        </div>
        
        <div class="footer-note">
           Mọi thắc mắc vui lòng liên hệ: <xsl:value-of select="PaymentStatement/ProviderInfo/Phone"/> - Kế toán: <xsl:value-of select="PaymentStatement/HeaderInfo/AccountantName"/>
        </div>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>