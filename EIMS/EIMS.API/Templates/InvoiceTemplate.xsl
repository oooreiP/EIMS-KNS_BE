<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" indent="yes" encoding="utf-8"/>
	<xsl:decimal-format name="vnd" decimal-separator="," grouping-separator="."/>

	<xsl:param name="ReferenceText" />
	<xsl:param name="LogoUrl" select="''"/>
	<xsl:param name="BackgroundUrl" select="''"/>
	<xsl:param name="ColorTheme" select="'#0056b3'"/>
	<xsl:param name="FontFamily" select="'Times New Roman'"/>
	<xsl:param name="IsBilingual" select="'false'"/>
	<xsl:param name="IsDraft" select="'false'"/>
	<xsl:param name="QrCodeData" select="''"/>	
	<xsl:param name="ShowQrCode" select="'true'"/>
	<xsl:param name="ShowLogo" select="'true'"/>
	<xsl:param name="ShowSignature" select="'true'"/>
	<xsl:param name="ShowCompanyName" select="'true'"/>
	<xsl:param name="ShowCompanyTaxCode" select="'true'"/>
	<xsl:param name="ShowCompanyAddress" select="'true'"/>
	<xsl:param name="ShowCompanyPhone" select="'true'"/>
	<xsl:param name="ShowCompanyBankAccount" select="'true'"/>
	<xsl:param name="ShowCusName" select="'true'"/>
	<xsl:param name="ShowCusTaxCode" select="'true'"/>
	<xsl:param name="ShowCusAddress" select="'true'"/>
	<xsl:param name="ShowCusPhone" select="'true'"/>
	<xsl:param name="ShowCusEmail" select="'true'"/>
	<xsl:param name="ShowPaymentMethod" select="'true'"/>

	<xsl:template match="/">
		<html>
			<head>
				<meta charset="UTF-8"/>
				<title>Hóa đơn điện tử</title>
				<style>
					/* Đã giảm font-size body từ 15px xuống 12px */
					body {
						font-family: '<xsl:value-of select="$FontFamily"/>', serif;
						font-size: 12px; 
						color: #333;
						margin: 0;
						padding: 0;
						line-height: 1.25;
					}
					.page-container {
						width: 210mm; min-height: 297mm; margin: auto; padding: 10mm 15mm;
						box-sizing: border-box; position: relative;
						background-repeat: no-repeat; background-position: center; background-size: 100% 100%;
					}

					/* HEADER */
					.header-table { width: 100%; border: none; margin-bottom: 5px; }
					.header-table td { vertical-align: top; }
					
					/* Giảm title từ 24px xuống 19px */
					.invoice-title {
						font-size: 24px; 
						margin: 0; text-transform: uppercase;
						font-weight: bold; letter-spacing: 1px; white-space: nowrap;
						color: #2C3452;
						line-height: 1.1;
					}
					/* INFO TABLE */
					.info-table { width: 100%; border-collapse: collapse; margin-bottom: 2px; }
					.info-label {
						font-size: 12px;
						width: 145px; vertical-align: top;
						padding: 1px 0;
					}
					.info-content {
						text-align: left; padding-left: 5px; font-size: 12px; font-weight: bold;
					}
					.info-divider { border-top: 1px solid #ddd; margin: 5px 0; }

					/* BODY TABLE */
					/* Giảm từ 15px xuống 12px */
					.body-table {
						width: 100%;
						border-collapse: collapse;
						margin-top: 10px;
						margin-bottom: 10px;
						font-size: 11px; 
					}
					/* Giảm từ 14px xuống 11px */
					.amount-words {
						width: 100%;
						text-align: left;
						margin-top: 15px;
						margin-bottom: 20px;
						font-size: 10px; 
						background: transparent;
						border: none;
						color: #000;
					}
					.body-table th {
						border: 1px solid #000;
						background-color: transparent;
						color: #000;
						padding: 6px 4px;
						font-weight: bold;
						text-align: center;
						vertical-align: middle;
					}
					.body-table td {
						border: 1px solid #000;
						padding: 5px 4px;
						vertical-align: top;
						color: #000;
					}
					/* Dòng trống */
					.empty-row td { height: 30px; border: 1px solid #000; } /* Giảm height chút */

					/* TOTAL TABLE */
					.total-table { width: 100%; border-collapse: collapse; }
					.total-label { text-align: right; font-weight: bold; padding: 2px 5px; }
					.total-value { text-align: right; padding: 2px 5px; }
					
					/* Giảm Grand total từ 18px xuống 14px */
					.grand-total { font-size: 14px; color: #d9534f; font-weight: bold; } 

					/* CHỮ KÝ */
					.signature-section {
						margin-top: 25px;
						display: flex; justify-content: space-between; page-break-inside: avoid;
					}
					.signature-box {
						margin-top: 5px; display:inline-block; padding: 5px 10px; border-radius: 4px;
						border: 2px solid <xsl:value-of select="$ColorTheme"/>;
						color: <xsl:value-of select="$ColorTheme"/>;
					}

					/* Utils */
					/* Giảm watermark từ 100px xuống 80px */
					.watermark {
						position: absolute; top: 50%; left: 50%;
						transform: translate(-50%, -50%) rotate(-45deg);
						font-size: 100px; color: rgba(255, 0, 0, 0.15);
						z-index: 0; pointer-events: none; white-space: nowrap; font-weight: bold;
					}
					.content-layer { position: relative; z-index: 1; }
					.italic { font-style: italic; font-weight: normal; font-size: 0.9em; }
					.text-center { text-align: center; }
					.text-right { text-align: right; }
					.text-left { text-align: left; }
					.text-bold { font-weight: bold; }
					.red-text { color: red; }
					/* CSS Class cho song ngữ */
					.en-label { font-weight: normal; font-style: italic; font-size: 0.9em; }
				</style>
			</head>
			<body>
				<div class="page-container">
					<xsl:if test="$BackgroundUrl != ''">
						<xsl:attribute name="style">
							background-image: url('<xsl:value-of select="$BackgroundUrl"/>');
						</xsl:attribute>
					</xsl:if>

					<xsl:if test="$IsDraft = 'true'">
						<div class="watermark">
							BẢN NHÁP 
							<xsl:if test="$IsBilingual = 'true'">(DRAFT)</xsl:if>
						</div>
					</xsl:if>

					<div class="content-layer">
						<xsl:apply-templates select="//*[local-name()='DLHDon']" />
					</div>
				</div>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="*[local-name()='DLHDon']">

		<table class="header-table">
			<tr>
				<td style="width: 15%; margin-top: -20px;">
					<xsl:if test="$ShowLogo = 'true' and $LogoUrl != ''">
						<img src="{$LogoUrl}" style="max-width: 100%; height: auto;" />
					</xsl:if>
				</td>
				<td style="width: 60%; text-align: center;">
					<div class="invoice-title" style="margin-top: 40px; ">HÓA ĐƠN GIÁ TRỊ GIA TĂNG</div>
					<xsl:if test="$IsBilingual = 'true'">
						<div class="italic" style="font-size: 13px; margin-top: 5px; color: #2C3452;">(VAT INVOICE)</div>
					</xsl:if>
					<xsl:if test="count(//*[local-name()='MCCQT' and string-length(text()) > 0]) > 0">
						<div style="margin-top: 2px; font-weight: bold;">
							Mã CQT <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Tax Code)</span></xsl:if>: <xsl:value-of select="(//*[local-name()='MCCQT' and string-length(text()) > 0])[1]"/>
						</div>
					</xsl:if>
					<div style="margin-top: 15px;font-size: 12px;">
						Ngày <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>
						tháng <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>
						năm <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>
						
						<xsl:if test="$IsBilingual = 'true'">
							<div class="italic" style="font-size: 12px;">
								(Date <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>
								month <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>
								year <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>)
							</div>
						</xsl:if>
					</div>
				</td>
				<td style="width: 25%; text-align: right; vertical-align: top;">
					<div style="margin-top: 80px; font-size: 11px;">
						<strong>Ký hiệu <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Symbol)</span></xsl:if>: </strong>
						<span style="white-space:nowrap;font-weight: bold;">
							<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='KHHDon']"/>
						</span>
					</div>

					<div style="font-size: 11px; margin-top: 4px;">
						<strong>Số <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(No.)</span></xsl:if>: </strong>
        
						<xsl:choose>
							<xsl:when test="string-length(*[local-name()='TTChung']/*[local-name()='SHDon']) > 0 and *[local-name()='TTChung']/*[local-name()='SHDon'] != '0'">
								<span style="color: red; font-weight: bold; font-size: 11px; white-space:nowrap">
									<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='SHDon']"/>
								</span>
							</xsl:when>
            
							<xsl:otherwise>
								<span style="
									display: inline-block;
									background-color: #fff9c4; 
									border: 1px solid #ffe082; 
									color: #e57373; 
									font-style: italic;
									font-weight: bold; 
									font-size: 10px; 
									padding: 1px 6px; 
									border-radius: 3px; 
									white-space: nowrap;
									margin-left: 2px;">
									BẢN NHÁP <xsl:if test="$IsBilingual = 'true'">(DRAFT)</xsl:if>
								</span>
							</xsl:otherwise>
						</xsl:choose>
					</div>
				</td>
			</tr>
			<xsl:if test="$ReferenceText != ''">
				<tr>
					<td colspan="3" style="text-align: center; padding-top: 5px; padding-bottom: 2px;">
						<div style="font-weight: bold; font-style: italic; font-size: 12px; color: #333;">
							<xsl:value-of select="$ReferenceText"/>
						</div>
					</td>
				</tr>
			</xsl:if>
		</table>
		<div class="info-divider"></div>

		<table style="width: 100%; border-collapse: collapse;">
	<tr>
		<td style="vertical-align: top; text-align: left;">

			<xsl:if test="$ShowCompanyName = 'true'">
				<div style="margin-bottom: 4px;">
					<span style="font-size: 12px;">
						Đơn vị bán <xsl:if test="$IsBilingual = 'true'">
							<span class="en-label">(Seller)</span>
						</xsl:if>:
					</span>
					<span style="font-weight: bold; text-transform: uppercase; font-size: 14px;">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='Ten']"/>
					</span>
				</div>
			</xsl:if>

			<table style="width: 100%; border-collapse: collapse; margin-bottom: 4px;">
				<tr>
					<td style="vertical-align: top; width: 50%; padding: 0;">
						<xsl:if test="$ShowCompanyTaxCode = 'true'">
							<div>
								<span style="font-size: 12px;">
									Mã số thuế <xsl:if test="$IsBilingual = 'true'">
										<span class="en-label">(Tax ID)</span>
									</xsl:if>:
								</span>
								<span style="font-weight: bold; font-size: 13px;">
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='MST']"/>
								</span>
							</div>
						</xsl:if>
					</td>

					<td style="vertical-align: top; width: 50%; padding: 0;">
						<xsl:if test="$ShowCompanyPhone = 'true'">
							<div>
								<span style="font-size: 12px;">
									Điện thoại <xsl:if test="$IsBilingual = 'true'">
										<span class="en-label">(Phone)</span>
									</xsl:if>:
								</span>
								<span style="font-weight: bold; font-size: 13px;">
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='SDThoai']"/>
								</span>
							</div>
						</xsl:if>
					</td>
				</tr>
			</table>
			
			<xsl:if test="$ShowCompanyAddress = 'true'">
				<div style="margin-bottom: 4px;">
					<span style="font-size: 12px;">
						Địa chỉ <xsl:if test="$IsBilingual = 'true'">
							<span class="en-label">(Address)</span>
						</xsl:if>:
					</span>
					<span style="font-weight: bold; font-size: 13px;">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='DChi']"/>
					</span>
				</div>
			</xsl:if>

			<xsl:if test="$ShowCompanyBankAccount = 'true'">
				<div style="margin-bottom: 4px;">
					<span style="font-size: 12px;">
						Số tài khoản <xsl:if test="$IsBilingual = 'true'">
							<span class="en-label">(Account No.)</span>
						</xsl:if>:
					</span>
					<span style="font-weight: bold; font-size: 13px;">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='STKNHang']"/>
						<xsl:if test="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='TNHang'] != ''">
							tại 
							<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='TNHang']"/>
						</xsl:if>
					</span>
				</div>
			</xsl:if>
		</td>

		<td style="width: 100px; vertical-align: top; text-align: right;">
			<xsl:if test="$ShowQrCode = 'true'">
				<div style="display: inline-block;">
					<xsl:if test="$QrCodeData != ''">
						<xsl:choose>
							<xsl:when test="starts-with($QrCodeData, 'data:image')">
								<img src="{$QrCodeData}" style="width: 65px; height: 65px;" />
							</xsl:when>
							<xsl:otherwise>
								<img src="data:image/png;base64,{$QrCodeData}" style="width: 65px; height: 65px;" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:if>
				</div>
			</xsl:if>
		</td>
	</tr>
</table>

		<div class="info-divider"></div>

		<table style="width: 100%; border-collapse: collapse; margin-bottom: 6px;">
  <tr>
    <td style="vertical-align: top; text-align: left;">

      <!-- Buyer Name -->
      <xsl:if test="$ShowCusName = 'true'">
        <div style="margin-bottom: 4px;">
          <span style="font-size: 12px;">
            Họ tên người mua hàng
            <xsl:if test="$IsBilingual = 'true'">
              <span class="en-label">(Buyer Name)</span>
            </xsl:if>:
          </span>
          <span style="font-weight: bold; font-size: 13px;">
            <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='HVTNMHang']"/>
          </span>
        </div>
      </xsl:if>

      <xsl:if test="$ShowCusName = 'true'">
        <div style="margin-bottom: 4px;">
          <span style="font-size: 12px;">
            Đơn vị mua
            <xsl:if test="$IsBilingual = 'true'">
              <span class="en-label">(Company Name)</span>
            </xsl:if>:
          </span>
          <span style="font-weight: bold; font-size: 13px;">
            <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='Ten']"/>
          </span>
        </div>
      </xsl:if>
      <table style="width: 100%; border-collapse: collapse; margin-bottom: 4px;">
        <tr>
			<td style="vertical-align: top; width: 43%; padding: 0;">
				<xsl:if test="$ShowCusTaxCode = 'true'
    and (
      normalize-space(*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='MST']) != ''
      or
      normalize-space(*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='CCCDan']) != ''
    )
  ">
					<div>
						<span style="font-size: 12px;">
							Mã số thuế/CCCD
							<xsl:if test="$IsBilingual = 'true'">
								<span class="en-label">(Tax ID/ID Card)</span>
							</xsl:if>:
						</span>
						<span style="font-weight: bold; font-size: 13px;">
							<xsl:choose>
								<xsl:when test="normalize-space(*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='MST']) != ''">
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='MST']"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='CCCDan']"/>
								</xsl:otherwise>
							</xsl:choose>
						</span>
					</div>
				</xsl:if>
			</td>

          <td style="vertical-align: top; width: 57%; padding-left: -50px;">
            <xsl:if test="$ShowCusPhone = 'true'">
              <div>
                <span style="font-size: 12px;">
                  Điện thoại
                  <xsl:if test="$IsBilingual = 'true'">
                    <span class="en-label">(Phone)</span>
                  </xsl:if>:
                </span>
                <span style="font-weight: bold; font-size: 13px;">
                  <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='SDThoai']"/>
                </span>
              </div>
            </xsl:if>
          </td>
        </tr>
      </table>

      <!-- Address -->
      <xsl:if test="$ShowCusAddress = 'true'">
        <div style="margin-bottom: 4px;">
          <span style="font-size: 12px;">
            Địa chỉ
            <xsl:if test="$IsBilingual = 'true'">
              <span class="en-label">(Address)</span>
            </xsl:if>:
          </span>
          <span style="font-weight: bold; font-size: 13px;">
            <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DChi']"/>
          </span>
        </div>
      </xsl:if>

      <!-- Email -->
      <xsl:if test="$ShowCusEmail = 'true'">
        <div style="margin-bottom: 4px;">
          <span style="font-size: 12px;">
            Email:
          </span>
          <span style="font-weight: bold; font-size: 13px;">
            <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DCTDTu']"/>
          </span>
        </div>
      </xsl:if>

      <!-- Payment Method -->
      <xsl:if test="$ShowPaymentMethod = 'true'">
        <div>
          <span style="font-size: 12px;">
            Hình thức thanh toán
            <xsl:if test="$IsBilingual = 'true'">
              <span class="en-label">(Payment method)</span>
            </xsl:if>:
          </span>
          <span style="font-weight: bold; font-size: 13px;">
            <xsl:value-of select="*[local-name()='TTChung']/*[local-name()='HTTToan']"/>
          </span>
        </div>
      </xsl:if>

    </td>
  </tr>
</table>
		<table class="body-table">
			<colgroup>
				<col style="width: 5%;" />
				<col style="width: 40%;" />
				<col style="width: 10%;" />
				<col style="width: 10%;" />
				<col style="width: 15%;" />
				<col style="width: 5%;" />
				<col style="width: 15%;" />
			</colgroup>

			<thead>
				<tr>
					<th>
						STT <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(No.)</span></xsl:if>
					</th>
					<th>
						Tên hàng hóa, dịch vụ <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Description)</span></xsl:if>
					</th>
					<th>
						ĐVT <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Unit)</span></xsl:if>
					</th>
					<th>
						Số lượng <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Quantity)</span></xsl:if>
					</th>
					<th>
						Đơn giá <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Unit Price)</span></xsl:if>
					</th>
					<th>
						Thuế suất <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(VAT%)</span></xsl:if>
					</th>
					<th>
						Thành tiền <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Amount)</span></xsl:if>
					</th>
				</tr>
			</thead>

			<tbody>
	<xsl:variable name="Items" select="*[local-name()='NDHDon']/*[local-name()='DSHHDVu']/*[local-name()='HHDVu']"/>
	<xsl:for-each select="$Items">
		<tr>
			<td class="text-center">
				<xsl:value-of select="*[local-name()='STT']"/>
			</td>
			<td class="text-left" style="font-weight: 500;">
				<xsl:value-of select="*[local-name()='THHDVu']"/>
			</td>
			<td class="text-center">
				<xsl:value-of select="*[local-name()='DVTinh']"/>
			</td>

			<td class="text-right">
				<xsl:variable name="nSLuong" select="number(*[local-name()='SLuong'])"/>
				<xsl:if test="$nSLuong != 0">
					<xsl:choose>
						<xsl:when test="$nSLuong &lt; 0">
							<span class="red-text">
								(<xsl:value-of select="format-number($nSLuong * -1, '###.##0,##', 'vnd')"/>)
							</span>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number($nSLuong, '###.##0,##', 'vnd')"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</td>

			<td class="text-right">
				<xsl:variable name="nDGia" select="number(*[local-name()='DGia'])"/>
				<xsl:if test="$nDGia != 0">
					<xsl:choose>
						<xsl:when test="$nDGia &lt; 0">
							<span class="red-text">
								(<xsl:value-of select="format-number($nDGia * -1, '###.##0,##', 'vnd')"/>)
							</span>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number($nDGia, '###.##0,##', 'vnd')"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</td>

			<td class="text-center">
			<xsl:variable name="TenHang" select="*[local-name()='THHDVu']"/>
			<xsl:variable name="ThueSuat" select="*[local-name()='TSuat']"/>

			<xsl:if test="$TenHang != '' and string-length($ThueSuat) > 0">
				<xsl:choose>
					<xsl:when test="starts-with($ThueSuat, 'K')">
						<xsl:value-of select="$ThueSuat"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$ThueSuat"/>%
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</td>

			<td class="text-right">
				<xsl:variable name="nThTien" select="number(*[local-name()='ThTien'])"/>
				<xsl:if test="$nThTien != 0">
					<xsl:choose>
						<xsl:when test="$nThTien &lt; 0">
							<span class="red-text">
								(<xsl:value-of select="format-number($nThTien * -1, '###.##0,##', 'vnd')"/>)
							</span>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number($nThTien, '###.##0,##', 'vnd')"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</td>					
		</tr>
	</xsl:for-each>

	<xsl:variable name="Count" select="count($Items)"/>

	<xsl:if test="$Count &lt; 3">
		<tr class="empty-row"><td/><td/><td/><td/><td/><td/><td/></tr>
	</xsl:if>
	<xsl:if test="$Count &lt; 2">
		<tr class="empty-row"><td/><td/><td/><td/><td/><td/><td/></tr>
	</xsl:if>

	<xsl:if test="$Count &lt; 5">
		<tr class="empty-row">
			<td></td>
			<td colspan="6" style="text-align: left; padding-left: 5px; color: #444; font-style: italic;">
				<xsl:variable name="GhiChu" select="*[local-name()='NDHDon']/*[local-name()='GhiChu']"/>
				<xsl:choose>
					<xsl:when test="$GhiChu != ''">
						<xsl:value-of select="$GhiChu"/>
					</xsl:when>
					<xsl:otherwise>
						<span style="opacity: 0.6;">
							Nhập ghi chú nếu có 
							<xsl:if test="$IsBilingual = 'true'">(Notes if any)</xsl:if>
						</span>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
	</xsl:if>
</tbody>
		</table>

		<div style="page-break-inside: avoid;">
	<table style="width: 100%; border: none; border-collapse: collapse;">
		<tr>
			<td style="width: 50%;"></td>
			
			<td style="width: 50%; vertical-align: top;">
				
				<table class="total-table" style="width: 100%; border-collapse: collapse;">
					
					<tr>
						<td class="total-label" style="text-align: left; padding: 5px 0; font-size: 12px;">
							Tổng tiền hàng <xsl:if test="$IsBilingual = 'true'"> <span class="en-label" style="font-style: italic;">(Subtotal)</span></xsl:if>:
						</td>
						<td class="total-value" style="text-align: right; padding: 5px 0; font-weight: bold; font-size: 12px;">
							<xsl:variable name="nTienHang" select="number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTCThue'])"/>
							<xsl:if test="$nTienHang != 0">
								<xsl:value-of select="format-number($nTienHang, '###.##0,##', 'vnd')"/>
							</xsl:if>
						</td>
					</tr>

					<tr>
						<td class="total-label" style="text-align: left; padding: 5px 0; font-size: 12px;">
							Tiền thuế GTGT <xsl:if test="$IsBilingual = 'true'"> <span class="en-label" style="font-style: italic;">(VAT Amount)</span></xsl:if>:
						</td>
						<td class="total-value" style="text-align: right; padding: 5px 0; font-weight: bold; font-size: 12px;">
							<xsl:variable name="nTienThue" select="number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTThue'])"/>
							<xsl:if test="$nTienThue != 0">
								<xsl:value-of select="format-number($nTienThue, '###.##0,##', 'vnd')"/>
							</xsl:if>
						</td>
					</tr>

					<tr>
						<td class="total-label" style="text-align: left; padding: 10px 0; font-weight: bold; font-size: 14px; border-top: 1px solid #ccc;">
							Tổng tiền thanh toán <xsl:if test="$IsBilingual = 'true'"> <span class="en-label" style="font-style: italic;">(Total Amount)</span></xsl:if>:
						</td>
						<td class="total-value" style="text-align: right; padding: 10px 0; font-weight: bold; font-size: 14px; border-top: 1px solid #ccc;">
							<xsl:variable name="nTongTien" select="number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBSo'])"/>
							<xsl:if test="$nTongTien != 0">
								<xsl:value-of select="format-number($nTongTien, '###.##0,##', 'vnd')"/>
							</xsl:if>
						</td>
					</tr>

				</table>
			</td>
		</tr>
	</table>

			<div class="amount-words" style="margin-top: 10px; font-style: italic;">
				Số tiền viết bằng chữ <xsl:if test="$IsBilingual = 'true'"><span class="en-label"> (Amount in words)</span></xsl:if>: <b>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBChu']"/>
				</b>
			</div>
		</div>

		<xsl:if test="$ShowSignature = 'true'">
			<div class="signature-section">
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">
						Người mua hàng <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Buyer)</span></xsl:if>
					</div>
					<div class="italic">
						(Chữ ký số (nếu có) <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Digital signature (if any))</span></xsl:if>
					</div>
				</div>
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">
						Người bán hàng <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Seller)</span></xsl:if>
					</div>
					<div class="italic">
						(Chữ ký điện tử, Chữ ký số) <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Digital signature)</span></xsl:if>
					</div>

					<xsl:choose>
						<xsl:when test="string-length((//*[local-name()='SignatureValue'])[1]) > 0">
							<div class="signature-box">
								<div style="font-weight: bold; text-transform: uppercase;">
									Đã ký điện tử bởi <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label" style="text-transform: none;">(Signed digitally by)</span></xsl:if>
								</div>
								<div style="margin-top: 2px; font-weight: bold;">
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='Ten']"/>
								</div>
								<div style="font-size: 9px; margin-top: 2px;">
									Ngày ký <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Signed date)</span></xsl:if>:
									<xsl:choose>
										<xsl:when test="string-length((//*[local-name()='SigningTime'])[1]) > 0">
											<xsl:variable name="sTime" select="(//*[local-name()='SigningTime'])[1]"/>
											<xsl:value-of select="substring($sTime, 9, 2)"/>/<xsl:value-of select="substring($sTime, 6, 2)"/>/<xsl:value-of select="substring($sTime, 1, 4)"/>
											<xsl:text> </xsl:text>
											<xsl:value-of select="substring($sTime, 12, 8)"/>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>/<xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>/<xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>
										</xsl:otherwise>
									</xsl:choose>
								</div>
								<div style="color: green; font-size: 13px;">✔</div>
							</div>
						</xsl:when>
						<xsl:otherwise>
							<div style="margin-top:60px; color:#ccc;">
								(Chưa ký) <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Not signed)</span></xsl:if>
							</div>
						</xsl:otherwise>
					</xsl:choose>
				</div>
			</div>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>