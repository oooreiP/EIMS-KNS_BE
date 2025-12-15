<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:hdon="http://tempuri.org/HDonSchema.xsd" 
    exclude-result-prefixes="hdon">
    
    <xsl:output method="html" indent="yes" encoding="utf-8"/>
    <xsl:decimal-format name="vnd" decimal-separator="," grouping-separator="."/>

    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Hóa đơn điện tử</title>
                <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700&amp;display=swap" rel="stylesheet"/>
                <style>
                    body { font-family: 'Montserrat', sans-serif; font-size: 14px; color: #333; margin: 0; padding: 20px; background: #f5f5f5; }
                    .invoice-box { max-width: 800px; margin: auto; background: #fff; padding: 30px; border: 1px solid #eee; box-shadow: 0 0 10px rgba(0, 0, 0, 0.15); }
                    
                    /* Header */
                    .header { text-align: center; margin-bottom: 20px; }
                    .header h1 { font-size: 24px; color: #0056b3; margin: 5px 0; text-transform: uppercase; }
                    .header p { margin: 2px 0; }
                    .serial-info { color: #d9534f; font-weight: bold; margin-top: 10px; }

                    /* Info Tables */
                    .info-table { width: 100%; margin-bottom: 20px; border-collapse: collapse; }
                    .info-table td { vertical-align: top; padding: 5px; }
                    .info-title { font-weight: bold; color: #0056b3; font-size: 15px; border-bottom: 2px solid #eee; padding-bottom: 5px; margin-bottom: 5px; display: block;}
                    .label { font-weight: 600; width: 100px; display: inline-block; }

                    /* Items Table */
                    .items-table { width: 100%; border-collapse: collapse; margin-top: 20px; }
                    .items-table th { background: #0056b3; color: white; font-weight: 600; padding: 10px; text-align: center; border: 1px solid #004494; }
                    .items-table td { border: 1px solid #ddd; padding: 8px; font-size: 13px; }
                    .text-center { text-align: center; }
                    .text-right { text-align: right; }
                    .items-table tr:nth-child(even) { background-color: #f9f9f9; }

                    /* Footer Totals */
                    .totals-box { margin-top: 20px; text-align: right; }
                    .totals-box table { float: right; width: 50%; border-collapse: collapse; }
                    .totals-box td { padding: 5px; }
                    .total-label { font-weight: bold; }
                    .grand-total { font-size: 18px; color: #d9534f; font-weight: bold; }
                    
                    /* Signatures */
                    .signatures { margin-top: 40px; width: 100%; overflow: hidden; }
                    .sig-block { width: 45%; float: left; text-align: center; }
                    .sig-block.right { float: right; }
                    .sig-title { font-weight: bold; margin-bottom: 50px; text-transform: uppercase; }
                    
                    /* Clearfix */
                    .clearfix::after { content: ""; clear: both; display: table; }
                </style>
            </head>
            <body>
                <div class="invoice-box">
                    <xsl:apply-templates select="//hdon:DLHDon" />
                </div>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="hdon:DLHDon">
        
        <div class="header">
            <h1>HÓA ĐƠN GIÁ TRỊ GIA TĂNG</h1>
            <p><i>(Bản thể hiện của hóa đơn điện tử)</i></p>
            <div class="serial-info">
                Ký hiệu: <xsl:value-of select="hdon:TTChung/hdon:KHHDon"/> &#160;|&#160; 
                Số: <xsl:value-of select="hdon:TTChung/hdon:SHDon"/> &#160;|&#160; 
                Ngày: <xsl:value-of select="substring(hdon:TTChung/hdon:NLap, 9, 2)"/>/<xsl:value-of select="substring(hdon:TTChung/hdon:NLap, 6, 2)"/>/<xsl:value-of select="substring(hdon:TTChung/hdon:NLap, 1, 4)"/>
            </div>
        </div>

        <table class="info-table">
            <tr>
                <td width="50%">
                    <div class="info-title">ĐƠN VỊ BÁN HÀNG (Seller)</div>
                    <div><span class="label">Đơn vị:</span> <b style="text-transform:uppercase"><xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:Ten"/></b></div>
                    <div><span class="label">Mã số thuế:</span> <xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:MST"/></div>
                    <div><span class="label">Địa chỉ:</span> <xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:DChi"/></div>
                    <div><span class="label">Điện thoại:</span> <xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:SDThoai"/></div>
                    <div><span class="label">Số TK:</span> <xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:STKNHang"/> <br/> tại <xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:TNHang"/></div>
                </td>
                <td width="50%">
                    <div class="info-title">NGƯỜI MUA HÀNG (Buyer)</div>
                    <div><span class="label">Họ tên:</span> <xsl:value-of select="hdon:NDHDon/hdon:NMua/hdon:Ten"/></div>
                    <div><span class="label">Đơn vị:</span> <xsl:value-of select="hdon:NDHDon/hdon:NMua/hdon:Ten"/></div>
                    <div><span class="label">Mã số thuế:</span> <xsl:value-of select="hdon:NDHDon/hdon:NMua/hdon:MST"/></div>
                    <div><span class="label">Địa chỉ:</span> <xsl:value-of select="hdon:NDHDon/hdon:NMua/hdon:DChi"/></div>
                    <div><span class="label">Email:</span> <xsl:value-of select="hdon:NDHDon/hdon:NMua/hdon:DCTDTu"/></div>
                </td>
            </tr>
        </table>

        <table class="items-table">
            <thead>
                <tr>
                    <th width="5%">STT</th>
                    <th>Tên hàng hóa, dịch vụ</th>
                    <th width="8%">ĐVT</th>
                    <th width="10%">Số lượng</th>
                    <th width="15%">Đơn giá</th>
                    <th width="15%">Thành tiền</th>
                    <th width="8%">Thuế</th>
                </tr>
            </thead>
            <tbody>
                <xsl:for-each select="hdon:NDHDon/hdon:DSHHDVu/hdon:HHDVu">
                    <tr>
                        <td class="text-center"><xsl:value-of select="hdon:STT"/></td>
                        <td><xsl:value-of select="hdon:THHDVu"/></td>
                        <td class="text-center"><xsl:value-of select="hdon:DVTinh"/></td>
                        <td class="text-right"><xsl:value-of select="format-number(hdon:SLuong, '###.##0,##', 'vnd')"/></td>
                        <td class="text-right"><xsl:value-of select="format-number(hdon:DGia, '###.##0,##', 'vnd')"/></td>
                        <td class="text-right"><xsl:value-of select="format-number(hdon:ThTien, '###.##0,##', 'vnd')"/></td>
                        <td class="text-center"><xsl:value-of select="hdon:TSuat"/>%</td>
                    </tr>
                </xsl:for-each>
                
                <xsl:if test="count(hdon:NDHDon/hdon:DSHHDVu/hdon:HHDVu) &lt; 5">
                    <tr><td colspan="7" style="height:30px; border:none;"></td></tr>
                </xsl:if>
            </tbody>
        </table>

        <div class="clearfix">
            <div class="totals-box">
                <table>
                    <tr>
                        <td class="total-label">Cộng tiền hàng:</td>
                        <td class="text-right"><xsl:value-of select="format-number(hdon:NDHDon/hdon:TToan/hdon:TgTCThue, '###.##0,##', 'vnd')"/></td>
                    </tr>
                    <tr>
                        <td class="total-label">Tiền thuế GTGT:</td>
                        <td class="text-right"><xsl:value-of select="format-number(hdon:NDHDon/hdon:TToan/hdon:TgTThue, '###.##0,##', 'vnd')"/></td>
                    </tr>
                    <tr>
                        <td class="total-label" style="border-top:1px solid #ccc; padding-top:10px;">TỔNG CỘNG THANH TOÁN:</td>
                        <td class="text-right grand-total" style="border-top:1px solid #ccc; padding-top:10px;">
                            <xsl:value-of select="format-number(hdon:NDHDon/hdon:TToan/hdon:TgTTTBSo, '###.##0,##', 'vnd')"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <div style="clear:both; margin-top:10px; font-style:italic;">
            (Số tiền bằng chữ: <xsl:value-of select="hdon:NDHDon/hdon:TToan/hdon:TgTTTBChu"/>)
        </div>

        <div class="signatures">
            <div class="sig-block">
                <div class="sig-title">Người mua hàng</div>
                <div style="margin-top:50px;">
                    (Ký qua email/SMS)
                </div>
            </div>
            <div class="sig-block right">
                <div class="sig-title">Người bán hàng</div>
                
                <xsl:choose>
                    <xsl:when test="string-length(../../hdon:DSCKS/hdon:NBan/hdon:Signature/hdon:SignatureValue) > 0 or string-length(../hdon:DSCKS/hdon:NBan/hdon:Signature/hdon:SignatureValue) > 0">
                        <div style="border: 2px solid #0056b3; color: #0056b3; display:inline-block; padding: 10px; margin-top:20px; border-radius: 4px;">
                            Đã ký điện tử bởi<br/>
                            <b><xsl:value-of select="hdon:NDHDon/hdon:NBan/hdon:Ten"/></b><br/>
                            Ngày: <xsl:value-of select="hdon:TTChung/hdon:NLap"/>
                        </div>
                    </xsl:when>
                    <xsl:otherwise>
                        <div style="margin-top:60px; color:#999;">(Chưa ký)</div>
                    </xsl:otherwise>
                </xsl:choose>
            </div>
        </div>

    </xsl:template>
</xsl:stylesheet>