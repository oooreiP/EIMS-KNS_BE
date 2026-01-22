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
					body {
					font-family: '<xsl:value-of select="$FontFamily"/>', serif;
					font-size: 15px; 
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
					.invoice-title {
					font-size: 24px; 
					margin: 0; text-transform: uppercase;
					font-weight: bold; letter-spacing: 1px; white-space: nowrap;
					color: #333;
					line-height: 1.1;
					}
					/* INFO TABLE */
					.info-table { width: 100%; border-collapse: collapse; margin-bottom: 2px; }
					.info-label {
					width: 145px; vertical-align: top;
					padding: 1px 0; 
					}
					.info-content {
					display: inline; margin-left: 5px; 
					}
					.info-divider { border-top: 1px solid #ddd; margin: 5px 0; }

					/* BODY TABLE */
					.body-table {
					width: 100%;
					border-collapse: collapse;
					margin-top: 10px;
					margin-bottom: 10px;
					font-size: 15px; 
					}
					.amount-words {
					width: 100%;
					text-align: left; 
					margin-top: 15px; 
					margin-bottom: 20px;
					font-size: 14px; 
					background: transparent; 
					border: none; 
					color: #000;
					}
					.body-table th {
					border: 1px solid #000;
					background-color: #f5f5f5; 
					color: #333; 
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
					.empty-row td { height: 35px; border: 1px solid #000; }

					/* TOTAL TABLE */
					.total-table { width: 100%; border-collapse: collapse; }
					.total-label { text-align: right; font-weight: bold; padding: 2px 5px; } 
					.total-value { text-align: right; padding: 2px 5px; }
					.grand-total { font-size: 18px; color: #d9534f; font-weight: bold; } 

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
				<td style="width: 20%; margin-top: -20px;">
					<xsl:if test="$ShowLogo = 'true' and $LogoUrl != ''">
						<img src="{$LogoUrl}" style="max-width: 100%; height: auto;" />
					</xsl:if>
				</td>
				<td style="width: 55%; text-align: center;">
					<div class="invoice-title" style="margin-top: 40px;">HÓA ĐƠN GIÁ TRỊ GIA TĂNG</div>
					<xsl:if test="$IsBilingual = 'true'">
						<div class="italic" style="font-size: 16px; margin-top: 5px;">(VAT INVOICE)</div>
					</xsl:if>
					<xsl:if test="count(//*[local-name()='MCCQT' and string-length(text()) > 0]) > 0">
						<div style="margin-top: 2px; font-weight: bold;">
							Mã CQT <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Tax Authority Code)</span></xsl:if>: <xsl:value-of select="(//*[local-name()='MCCQT' and string-length(text()) > 0])[1]"/>
						</div>
					</xsl:if>
					<div style="margin-top: 2px;">
						Ngày <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>
						tháng <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>
						năm <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>
						
						<xsl:if test="$IsBilingual = 'true'">
							<div class="italic" style="font-size: 13px;">
								(Date <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>
								month <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>
								year <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>)
							</div>
						</xsl:if>
					</div>
				</td>
				<td style="width: 25%; text-align: right; vertical-align: top;">
					<div style="margin-top: 80px;">
						<strong>Ký hiệu <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Symbol)</span></xsl:if>: </strong>
						<span style="white-space:nowrap">
							<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='KHHDon']"/>
						</span>
					</div>

					<div>
						<strong>Số <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(No.)</span></xsl:if>: </strong>
						<span style="color: red; font-weight: bold; font-size: 18px; white-space:nowrap">
							<xsl:choose>
								<xsl:when test="string-length(*[local-name()='TTChung']/*[local-name()='SHDon']) > 0 and *[local-name()='TTChung']/*[local-name()='SHDon'] != '0'">
									<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='SHDon']"/>
								</xsl:when>
								<xsl:otherwise>BẢN NHÁP <xsl:if test="$IsBilingual = 'true'">(DRAFT)</xsl:if></xsl:otherwise>
							</xsl:choose>
						</span>
					</div>
				</td>
			</tr>
			<xsl:if test="$ReferenceText != ''">
				<tr>
					<td colspan="3" style="text-align: center; padding-top: 5px; padding-bottom: 2px;">
						<div style="font-weight: bold; font-style: italic; font-size: 15px; color: #333;">
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
							<span style="font-weight: bold;">
								Đơn vị bán <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Seller)</span></xsl:if>:
							</span>
							<span style="font-weight: bold; text-transform: uppercase; font-size: 15px;">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='Ten']"/>
							</span>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyTaxCode = 'true'">
						<div style="margin-bottom: 4px;">
							<span style="font-weight: bold;">
								Mã số thuế <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Tax code)</span></xsl:if>:
							</span>
							<span style="font-weight: bold;">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='MST']"/>
							</span>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyAddress = 'true'">
						<div style="margin-bottom: 4px;">
							<span style="font-weight: bold;">
								Địa chỉ <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Address)</span></xsl:if>:
							</span>
							<span style="font-weight: bold;">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='DChi']"/>
							</span>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyPhone = 'true'">
						<div style="margin-bottom: 4px;">
							<span style="font-weight: bold;">
								Điện thoại <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Phone)</span></xsl:if>:
							</span>
							<span style="font-weight: bold;">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='SDThoai']"/>
							</span>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyBankAccount = 'true'">
						<div style="margin-bottom: 4px;">
							<span style="font-weight: bold;">
								Số tài khoản <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Account No.)</span></xsl:if>:
							</span>
							<span style="font-weight: bold;">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='STKNHang']"/>
								<xsl:if test="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='TNHang'] != ''">
									tại <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(at) </span></xsl:if>
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
										<img src="{$QrCodeData}" style="width: 80px; height: 80px;" />
									</xsl:when>
									<xsl:otherwise>
										<img src="data:image/png;base64,{$QrCodeData}" style="width: 80px; height: 80px;" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:if>
						</div>
					</xsl:if>
				</td>
			</tr>
		</table>

		<div class="info-divider"></div>

		<table class="info-table">
			<xsl:if test="$ShowCusName = 'true'">
				<tr>
					<td class="info-label">
						Người mua hàng <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Buyer)</span></xsl:if>:
					</td>
					<td class="info-content text-bold">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='HVTNMHang']"/>
					</td>
				</tr>
				<tr>
					<td class="info-label">
						Tên đơn vị <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Company name)</span></xsl:if>:
					</td>
					<td class="info-content text-bold" style="font-size: 16px;">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='Ten']"/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="$ShowCusTaxCode = 'true'">
				<tr>
					<td class="info-label">
						Mã số thuế <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Tax code)</span></xsl:if>:
					</td>
					<td class="info-content">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='MST']"/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="$ShowCusAddress = 'true'">
				<tr>
					<td class="info-label">
						Địa chỉ <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Address)</span></xsl:if>:
					</td>
					<td class="info-content">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DChi']"/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="$ShowCusPhone = 'true'">
				<tr>
					<td class="info-label">
						Số điện thoại <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Phone)</span></xsl:if>:
					</td>
					<td class="info-content">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='SDThoai']"/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="$ShowCusEmail = 'true'">
				<tr>
					<td class="info-label">
						Email <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Email)</span></xsl:if>:
					</td>
					<td class="info-content">
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DCTDTu']"/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="$ShowPaymentMethod = 'true'">
				<tr>
					<td class="info-label">
						HT thanh toán <xsl:if test="$IsBilingual = 'true'"><span class="en-label">(Payment method)</span></xsl:if>:
					</td>
					<td class="info-content">
						<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='HTTToan']"/>
					</td>
				</tr>
			</xsl:if>
		</table>

		<table class="body-table">
			<colgroup>
				<col style="width: 5%;" />
				<col style="width: 40%;" />
				<col style="width: 8%;" />
				<col style="width: 10%;" />
				<col style="width: 12%;" />
				<col style="width: 15%;" />
				<col style="width: 10%;" />
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
							<xsl:choose>
								<xsl:when test="*[local-name()='SLuong'] &lt; 0">
									<span class="red-text">
										(<xsl:value-of select="format-number(*[local-name()='SLuong'] * -1, '###.##0,##', 'vnd')"/>)
									</span>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(*[local-name()='SLuong'], '###.##0,##', 'vnd')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td class="text-right">
							<xsl:choose>
								<xsl:when test="*[local-name()='DGia'] &lt; 0">
									<span class="red-text">
										(<xsl:value-of select="format-number(*[local-name()='DGia'] * -1, '###.##0,##', 'vnd')"/>)
									</span>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(*[local-name()='DGia'], '###.##0,##', 'vnd')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td class="text-center">
							<xsl:choose>
								<xsl:when test="starts-with(*[local-name()='TSuat'], 'K')">
									<xsl:value-of select="*[local-name()='TSuat']"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="*[local-name()='TSuat']"/>%
								</xsl:otherwise>
							</xsl:choose>
						</td>
						<td class="text-right">
							<xsl:choose>
								<xsl:when test="*[local-name()='ThTien'] &lt; 0">
									<span class="red-text">
										(<xsl:value-of select="format-number(*[local-name()='ThTien'] * -1, '###.##0,##', 'vnd')"/>)
									</span>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(*[local-name()='ThTien'], '###.##0,##', 'vnd')"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>					
					</tr>
				</xsl:for-each>

				<xsl:variable name="Count" select="count($Items)"/>
				<xsl:if test="$Count &lt; 4">
					<tr class="empty-row"><td/><td/><td/><td/><td/><td/><td/></tr>
				</xsl:if>
				<xsl:if test="$Count &lt; 3">
					<tr class="empty-row"><td/><td/><td/><td/><td/><td/><td/></tr>
				</xsl:if>
				<xsl:if test="$Count &lt; 2">
					<tr class="empty-row"><td/><td/><td/><td/><td/><td/><td/></tr>
				</xsl:if>

				<xsl:if test="$Count &lt; 5">
					<tr class="empty-row">
						<td></td>
						
						<td style="text-align: left; padding-left: 5px; color: #444; font-style: italic;">
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
						
						<td></td>
						<td></td>
						<td></td>
						<td></td>
						<td></td>
					</tr>
				</xsl:if>
			</tbody>
		</table>

		<div style="page-break-inside: avoid;">
			<table style="width: 100%; border: none;">
				<tr>
					<td style="width: 60%;"></td>
					<td style="width: 40%;">
						<table class="total-table">
							<tr>
								<td class="total-label">
									Cộng tiền hàng <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Total amount)</span></xsl:if>:
								</td>
								<td class="total-value">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTCThue'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
							<tr>
								<td class="total-label">
									Tiền thuế GTGT <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(VAT amount)</span></xsl:if>:
								</td>
								<td class="total-value">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTThue'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
							<tr>
								<td class="total-label" style="border-top: 1px solid #ccc; color: #0056b3;">
									TỔNG CỘNG <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(TOTAL PAYMENT)</span></xsl:if>:
								</td>
								<td class="total-value grand-total" style="border-top: 1px solid #ccc;">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBSo'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div class="amount-words">
				(Số tiền viết bằng chữ <xsl:if test="$IsBilingual = 'true'"><span class="en-label"> (Amount in words)</span></xsl:if>: <b>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBChu']"/>
				</b>)
			</div>
		</div>

		<xsl:if test="$ShowSignature = 'true'">
			<div class="signature-section">
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">
						NGƯỜI MUA HÀNG <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(BUYER)</span></xsl:if>
					</div>
					<div class="italic">
						(Chữ ký số (nếu có)) <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Digital signature (if any))</span></xsl:if>
					</div>
				</div>
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">
						NGƯỜI BÁN HÀNG <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(SELLER)</span></xsl:if>
					</div>
					<div class="italic">
						(Chữ ký điện tử, Chữ ký số) <xsl:if test="$IsBilingual = 'true'"><br/><span class="en-label">(Electronic signature, Digital signature)</span></xsl:if>
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
								<div style="font-size: 11px; margin-top: 2px;">
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
								<div style="color: green; font-size: 16px;">✔</div>
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