<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" indent="yes" encoding="utf-8"/>
	<xsl:decimal-format name="vnd" decimal-separator="," grouping-separator="."/>

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
					font-size: 14px; color: #333; margin: 0; padding: 0;
					}
					.page-container {
					width: 210mm; min-height: 297mm; margin: auto; padding: 10mm 15mm;
					box-sizing: border-box; position: relative;
					background-repeat: no-repeat; background-position: center; background-size: 100% 100%;
					}
					/* Header */
					.header-table { width: 100%; border: none; margin-bottom: 20px; }
					.header-table td { vertical-align: top; }
					.invoice-title { font-size: 24px; margin: 0; text-transform: uppercase; font-weight: bold; color: <xsl:value-of select="$ColorTheme"/>; }

					/* Info Section (Sửa lại style cho list dọc) */
					.seller-table { width: 100%; border: none; margin-bottom: 10px; }
					.seller-table td { vertical-align: top; }
					.info-row { margin-bottom: 4px; line-height: 1.6; }

					.label { font-weight: bold; }

					/* Items Table */
					.items-table { width: 100%; border-collapse: collapse; margin-top: 15px; }
					.items-table th {
					color: white; font-weight: bold; padding: 8px; text-align: center; border: 1px solid #000;
					background-color: <xsl:value-of select="$ColorTheme"/>;
					}
					.items-table td { border: 1px solid #000; padding: 8px; font-size: 13px; }
					.text-center { text-align: center; }
					.text-right { text-align: right; }
					.italic { font-style: italic; font-weight: normal; font-size: 0.9em; }
					.grand-total { font-size: 16px; color: #d9534f; font-weight: bold; }

					/* Signature Box */
					.signature-box {
					margin-top: 20px; display:inline-block; padding: 10px; border-radius: 4px;
					border: 2px solid <xsl:value-of select="$ColorTheme"/>;
					color: <xsl:value-of select="$ColorTheme"/>;
					}

					.watermark {
					position: absolute; top: 50%; left: 50%;
					transform: translate(-50%, -50%) rotate(-45deg);
					font-size: 100px; color: rgba(255, 0, 0, 0.15);
					z-index: 0; pointer-events: none; white-space: nowrap; font-weight: bold;
					}
					.content-layer { position: relative; z-index: 1; }

					/* Helper Divider */
					hr.divider { border: 0; border-top: 1px solid #ddd; margin: 10px 0; }
				</style>
			</head>
			<body>
				<div class="page-container">
					<xsl:if test="$BackgroundUrl != ''">
						<xsl:attribute name="style">
							background-image: url('<xsl:value-of select="$BackgroundUrl"/>');
						</xsl:attribute>
					</xsl:if>

					<xsl:if test="$IsDraft = 'true' or string-length((//SHDon)[1]) = 0">
						<div class="watermark">BẢN NHÁP</div>
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
				<td style="width: 30%;">
					<xsl:if test="$ShowLogo = 'true' and $LogoUrl != ''">
						<img src="{$LogoUrl}" style="max-width: 150px; height: auto;" />
					</xsl:if>
				</td>
				<td style="width: 40%; text-align: center;">
					<div class="invoice-title">HÓA ĐƠN GIÁ TRỊ GIA TĂNG</div>
					<xsl:if test="$IsBilingual = 'true'">
						<div class="italic">(VAT INVOICE)</div>
					</xsl:if>
					<xsl:if test="count(//*[local-name()='MCCQT' and string-length(text()) > 0]) > 0">
						<div style="margin-top: 5px; font-weight: bold;">
							Mã CQT: <xsl:value-of select="(//*[local-name()='MCCQT' and string-length(text()) > 0])[1]"/>
						</div>
					</xsl:if>
					<div>
						Ngày <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 9, 2)"/>
						tháng <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 6, 2)"/>
						năm <xsl:value-of select="substring(*[local-name()='TTChung']/*[local-name()='NLap'], 1, 4)"/>
					</div>
				</td>
				<td style="width: 30%; text-align: right;">
					<div>
						<strong>Ký hiệu: </strong>
						<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='KHHDon']"/>
					</div>
					<div>
						<strong>Số: </strong>
						<span style="color: red; font-weight: bold; font-size: 16px;">
							<xsl:choose>
								<xsl:when test="string-length(*[local-name()='TTChung']/*[local-name()='SHDon']) > 0 and *[local-name()='TTChung']/*[local-name()='SHDon'] != '0'">
									<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='SHDon']"/>
								</xsl:when>
								<xsl:otherwise>BẢN NHÁP</xsl:otherwise>
							</xsl:choose>
						</span>
					</div>
				<xsl:if test="$ShowQrCode = 'true'">
						<div style="margin-top: 5px;">
							<xsl:if test="$QrCodeData != ''">
								<xsl:choose>
									<xsl:when test="starts-with($QrCodeData, 'data:image')">
										 <img src="{$QrCodeData}" style="width: 70px; height: 70px;" />
									</xsl:when>
									<xsl:otherwise>
										 <img src="data:image/png;base64,{$QrCodeData}" style="width: 70px; height: 70px;" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:if>
            
							<xsl:if test="$QrCodeData = ''">
								<div style="display: inline-block; border: 1px dashed #999; padding: 5px; background: #f9f9f9;">
									<div style="width: 60px; height: 60px; line-height: 60px; text-align: center; font-size: 10px; color: #666;">QR CODE</div>
								</div>
							</xsl:if>
						</div>
					</xsl:if>
				</td>
			</tr>
		</table>

		<table class="seller-table">
			<tr>
				<td style="width: 75%;">
					<xsl:if test="$ShowCompanyName = 'true'">
						<div class="info-row">
							<span class="label">
								Đơn vị bán <xsl:if test="$IsBilingual='true'">
									<span class="italic">(Seller)</span>
								</xsl:if>:
							</span>
							<b style="text-transform:uppercase">
								<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='Ten']"/>
							</b>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyTaxCode = 'true'">
						<div class="info-row">
							<span class="label">
								Mã số thuế <xsl:if test="$IsBilingual='true'">
									<span class="italic">(Tax ID)</span>
								</xsl:if>:
							</span>
							<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='MST']"/>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyAddress = 'true'">
						<div class="info-row">
							<span class="label">
								Địa chỉ <xsl:if test="$IsBilingual='true'">
									<span class="italic">(Address)</span>
								</xsl:if>:
							</span>
							<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='DChi']"/>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyPhone = 'true'">
						<div class="info-row">
							<span class="label">
								Điện thoại <xsl:if test="$IsBilingual='true'">
									<span class="italic">(Phone)</span>
								</xsl:if>:
							</span>
							<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='SDThoai']"/>
						</div>
					</xsl:if>

					<xsl:if test="$ShowCompanyBankAccount = 'true'">
						<div class="info-row">
							<span class="label">
								Số TK <xsl:if test="$IsBilingual='true'">
									<span class="italic">(Account No.)</span>
								</xsl:if>:
							</span> <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='STKNHang']"/> <br/> tại <xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='TNHang']"/>
						</div>
					</xsl:if>
				</td>
			</tr>
		</table>

		<hr class="divider" />

		<div style="margin-bottom: 15px;">
			<xsl:if test="$ShowCusName = 'true'">
				<div class="info-row">
					<span class="label">
						Họ tên người mua hàng <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Buyer Name)</span>
						</xsl:if>:
					</span>
					<b>
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='HVTNMHang']"/>
					</b>
				</div>
				<div class="info-row">
					<span class="label">
						Tên đơn vị <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Company Name)</span>
						</xsl:if>:
					</span>
					<b>
						<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='Ten']"/>
					</b>
				</div>
			</xsl:if>

			<xsl:if test="$ShowCusTaxCode = 'true'">
				<div class="info-row">
					<span class="label">
						Mã số thuế <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Tax ID)</span>
						</xsl:if>:
					</span>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='MST']"/>
				</div>
			</xsl:if>

			<xsl:if test="$ShowCusAddress = 'true'">
				<div class="info-row">
					<span class="label">
						Địa chỉ <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Address)</span>
						</xsl:if>:
					</span>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DChi']"/>
				</div>
			</xsl:if>

			<xsl:if test="$ShowCusPhone = 'true'">
				<div class="info-row">
					<span class="label">
						Số điện thoại <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Phone)</span>
						</xsl:if>:
					</span>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='SDThoai']"/>
				</div>
			</xsl:if>

			<xsl:if test="$ShowCusEmail = 'true'">
				<div class="info-row">
					<span class="label">
						Email <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Email)</span>
						</xsl:if>:
					</span>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NMua']/*[local-name()='DCTDTu']"/>
				</div>
			</xsl:if>

			<xsl:if test="$ShowPaymentMethod = 'true'">
				<div class="info-row">
					<span class="label">
						Hình thức thanh toán <xsl:if test="$IsBilingual='true'">
							<span class="italic">(Payment Method)</span>
						</xsl:if>:
					</span>
					<xsl:value-of select="*[local-name()='TTChung']/*[local-name()='HTHDon']"/>
				</div>
			</xsl:if>
		</div>

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
				<xsl:for-each select="*[local-name()='NDHDon']/*[local-name()='DSHHDVu']/*[local-name()='HHDVu']">
					<tr>
						<td class="text-center">
							<xsl:value-of select="*[local-name()='STT']"/>
						</td>
						<td>
							<xsl:value-of select="*[local-name()='THHDVu']"/>
						</td>
						<td class="text-center">
							<xsl:value-of select="*[local-name()='DVTinh']"/>
						</td>
						<td class="text-right">
							<xsl:value-of select="format-number(*[local-name()='SLuong'], '###.##0,##', 'vnd')"/>
						</td>
						<td class="text-right">
							<xsl:value-of select="format-number(*[local-name()='DGia'], '###.##0,##', 'vnd')"/>
						</td>
						<td class="text-right">
							<xsl:value-of select="format-number(*[local-name()='ThTien'], '###.##0,##', 'vnd')"/>
						</td>
						<td class="text-center">
							<xsl:value-of select="*[local-name()='TSuat']"/>%
						</td>
					</tr>
				</xsl:for-each>
				<xsl:if test="count(*[local-name()='NDHDon']/*[local-name()='DSHHDVu']/*[local-name()='HHDVu']) &lt; 5">
					<tr>
						<td style="border:1px solid #000; height: 25px;"></td>
						<td style="border:1px solid #000;"></td>
						<td style="border:1px solid #000;"></td>
						<td style="border:1px solid #000;"></td>
						<td style="border:1px solid #000;"></td>
						<td style="border:1px solid #000;"></td>
						<td style="border:1px solid #000;"></td>
					</tr>
				</xsl:if>
			</tbody>
		</table>

		<div style="page-break-inside: avoid; margin-top: 15px;">
			<table style="width: 100%; border: none;">
				<tr>
					<td style="width: 60%;"></td>
					<td style="width: 40%;">
						<table style="width: 100%; border-collapse: collapse;">
							<tr>
								<td class="label">Cộng tiền hàng:</td>
								<td class="text-right">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTCThue'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
							<tr>
								<td class="label">Tiền thuế GTGT:</td>
								<td class="text-right">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTThue'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
							<tr>
								<td class="label" style="color:#0056b3; padding-top: 5px; border-top: 1px solid #ccc;">TỔNG CỘNG:</td>
								<td class="text-right grand-total" style="padding-top: 5px; border-top: 1px solid #ccc;">
									<xsl:value-of select="format-number(*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBSo'], '###.##0,##', 'vnd')"/>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div style="margin-top: 5px; font-style: italic; text-align: right;">
				(Số tiền bằng chữ: <b>
					<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='TToan']/*[local-name()='TgTTTBChu']"/>
				</b>)
			</div>
		</div>

		<xsl:if test="$ShowSignature = 'true'">
			<div style="margin-top: 40px; display: flex; justify-content: space-between; page-break-inside: avoid;">
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">NGƯỜI MUA HÀNG</div>
					<div class="italic">(Ký qua email/SMS)</div>
				</div>
				<div style="text-align: center; width: 45%;">
					<div class="label text-bold">NGƯỜI BÁN HÀNG</div>
					<div class="italic">(Ký, đóng dấu, ghi rõ họ tên)</div>

					<xsl:choose>
						<xsl:when test="string-length((//*[local-name()='SignatureValue'])[1]) > 0">
							<div class="signature-box">
								<div style="font-weight: bold; text-transform: uppercase;">Đã ký điện tử bởi</div>
								<div style="margin-top: 5px; font-weight: bold;">
									<xsl:value-of select="*[local-name()='NDHDon']/*[local-name()='NBan']/*[local-name()='Ten']"/>
								</div>
								<div style="font-size: 11px; margin-top: 5px;">
									Ngày ký:
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
								<div style="color: green; font-size: 20px;">✔</div>
							</div>
						</xsl:when>
						<xsl:otherwise>
							<div style="margin-top:60px; color:#ccc;">(Chưa ký)</div>
						</xsl:otherwise>
					</xsl:choose>
				</div>
			</div>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>