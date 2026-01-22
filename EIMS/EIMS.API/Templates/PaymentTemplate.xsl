<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:decimal-format name="vn" decimal-separator="," grouping-separator="." />

	<xsl:param name="ColorTheme" select="'#0056b3'"/>

	<xsl:template match="/">
		<html>
			<head>
				<style>
					body { font-family: 'Times New Roman', sans-serif; font-size: 13px; }

					/* Header Styles */
					.header-container { display: flex; align-items: center; justify-content: space-between; margin-bottom: 20px; }
					.header-left { width: 30%; }
					.header-center { width: 40%; text-align: center; }
					.header-right { width: 30%; text-align: right;}
					.logo { max-height: 80px; max-width: 100%; }
					.title { font-size: 18px; font-weight: bold; margin: 10px 0; }
					.customer-info { margin-bottom: 15px; }

					/* Table Styles */
					table { width: 100%; border-collapse: collapse; margin-top: 10px; }
					th, td { border: 1px solid black; padding: 5px; vertical-align: middle; }
					th { background-color: #f0f0f0; text-align: center; font-weight: bold; }
					.text-right { text-align: right; }
					.text-center { text-align: center; }
					.text-bold { font-weight: bold; }

					/* Footer Layout Mới: Chia 2 cột (Ghi chú bên trái - Chữ ký bên phải) */
					.footer-container {
					margin-top: 15px;
					display: flex;
					justify-content: space-between;
					align-items: flex-start; /* Căn đỉnh trên */
					}
					.footer-left { width: 60%; }
					.footer-right { width: 38%; text-align: center; } /* Bên phải chứa chữ ký */

					.footer-note { font-style: italic; margin-bottom: 5px; font-size: 12px; }

					/* Style Chữ ký giống Hóa đơn điện tử */
					.sign-title { font-weight: bold; text-transform: uppercase; margin-bottom: 5px; }
					.sign-note { font-style: italic; font-size: 11px; margin-bottom: 5px; }

					.signature-box {
					margin-top: 5px;
					display: inline-block;
					padding: 10px 15px;
					border-radius: 4px;
					text-align: left; /* Nội dung bên trong khung căn trái */
					/* Sử dụng biến màu sắc */
					border: 2px solid <xsl:value-of select="$ColorTheme"/>;
					color: <xsl:value-of select="$ColorTheme"/>;
					}
					.sign-label { font-size: 11px; font-weight: bold; text-transform: uppercase; }
					.sign-name { font-weight: bold; margin-top: 5px; }
					.sign-date { font-size: 11px; margin-top: 3px; }

				</style>
			</head>
			<body>

				<div class="header-container">
					<div class="header-left">
						<img class="logo" src="{PaymentStatement/ProviderInfo/LogoUrl}" alt="Logo" />
					</div>
					<div class="header-center">
						<div class="text-bold" style="font-size:16px">
							<xsl:value-of select="PaymentStatement/ProviderInfo/Name"/>
						</div>
						<div>
							<xsl:value-of select="PaymentStatement/ProviderInfo/Address"/>
						</div>
						<div class="title">
							BẢNG KÊ THANH TOÁN<br/>(PAYMENT LIST)
						</div>
					</div>
					<div class="header-right"></div>
				</div>

				<div class="customer-info">
					<div>
						<b>Khách hàng (Customer):</b> <xsl:value-of select="PaymentStatement/HeaderInfo/CustomerName"/> (<xsl:value-of select="PaymentStatement/HeaderInfo/CustomerCode"/>)
					</div>
					<div>
						<b>Tháng (Month):</b>
						<xsl:value-of select="PaymentStatement/HeaderInfo/StatementMonth"/>
					</div>
				</div>

				<table>
					<thead>
						<tr>
							<th rowspan="2">
								Khoản mục<br/>(Description)
							</th>
							<th colspan="2">Thời gian (Period)</th>
							<th colspan="2">Chỉ số (Indicator)</th>
							<th rowspan="2">
								Khối lượng<br/>(Quantity)
							</th>
							<th rowspan="2">
								Đơn giá<br/>(Unit Price)
							</th>
							<th rowspan="2">
								Thành tiền<br/>(Amount)
							</th>
							<th colspan="2">Thuế GTGT (VAT)</th>
							<th rowspan="2">
								Tổng cộng kỳ này<br/>(Total amount)
							</th>
							<th rowspan="2">
								Tổng nợ kỳ trước<br/>(Previous liabilities)
							</th>
							<th rowspan="2">
								Tổng cộng<br/>(Total Payable)
							</th>
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
								<td>
									<xsl:value-of select="Description"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="PeriodFrom"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="PeriodTo"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="IndicatorOld"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="IndicatorNew"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="Quantity"/>
								</td>
								<td class="text-right">
									<xsl:value-of select="format-number(UnitPrice, '#.###', 'vn')"/>
								</td>
								<td class="text-right">
									<xsl:value-of select="format-number(Amount, '#.###', 'vn')"/>
								</td>
								<td class="text-center">
									<xsl:value-of select="VATRate"/>%
								</td>
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

				<div class="footer-container">

					<div class="footer-left">
						<div class="footer-note">
							* Nợ tháng trước vẫn còn thể hiện trên bảng kê nếu khách hàng thanh toán sau ngày ra bảng kê này.<br/>
							(Liabilities from the previous period will still appear on the payment list if the customer makes a payment after the date of this list)
						</div>

						<div class="footer-note">
							Mọi thắc mắc vui lòng liên hệ: <xsl:value-of select="PaymentStatement/ProviderInfo/Phone"/>
							<br/>
							Kế toán: <xsl:value-of select="PaymentStatement/HeaderInfo/AccountantName"/>
						</div>
					</div>

					<div class="footer-right">
						<div class="sign-title">Đại diện nhà cung cấp</div>
						<div class="sign-note">(Ký, đóng dấu, ghi rõ họ tên)</div>

						<div class="signature-box">
							<xsl:attribute name="style">
								margin-top: 5px;
								display: inline-block;
								padding: 10px 15px;
								border-radius: 4px;
								text-align: left;
								border: 2px solid <xsl:value-of select="$ColorTheme"/>;
								color: <xsl:value-of select="$ColorTheme"/>;
							</xsl:attribute>

							<div class="sign-label">Signature Valid</div>
							<div class="sign-label">Ký bởi (Signed by):</div>

							<div class="sign-name">
								<xsl:value-of select="PaymentStatement/ProviderInfo/Name"/>
							</div>

							<div class="sign-date">
								Ngày ký (Signed date): <xsl:value-of select="PaymentStatement/HeaderInfo/StatementDate"/>
							</div>
						</div>

					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>