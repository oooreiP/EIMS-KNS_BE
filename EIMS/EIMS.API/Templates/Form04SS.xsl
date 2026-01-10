<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" encoding="utf-8"/>

	<xsl:param name="CompanyName" select="''" />
	<xsl:param name="IsDraft" select="'false'"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Mẫu 04/SS-HĐĐT</title>
				<style>
					body { font-family: "Times New Roman", Times, serif; font-size: 14px; margin: 20px; line-height: 1.3; color: #000; }
					.container { max-width: 800px; margin: 0 auto; position: relative; }
					.header { text-align: center; margin-bottom: 20px; }
					.nation { font-weight: bold; font-size: 14px; }
					.motto { font-weight: bold; text-decoration: underline; font-size: 14px; margin-bottom: 10px; }
					.form-id { text-align: right; font-weight: bold; font-style: italic; font-size: 12px; margin-bottom: 10px; }
					.title { font-weight: bold; font-size: 16px; margin: 15px 0; text-transform: uppercase; }

					.info-table { width: 100%; border: none; margin-bottom: 15px; }
					.info-table td { border: none; padding: 3px 0; vertical-align: top; }

					.data-table { width: 100%; border-collapse: collapse; margin-top: 10px; }
					.data-table th, .data-table td { border: 1px solid black; padding: 5px; text-align: center; vertical-align: middle; font-size: 13px; }
					.data-table th { background-color: #f0f0f0; font-weight: bold; }
					.text-left { text-align: left !important; }

					.footer { margin-top: 30px; display: flex; justify-content: space-between; }
					.footer-col { width: 45%; text-align: center; }
					.signature-box { margin-top: 10px; padding: 5px; border: 2px dashed #0056b3; color: #0056b3; display: inline-block; font-size: 12px; }

					.watermark { position: absolute; top: 40%; left: 50%; transform: translate(-50%, -50%) rotate(-45deg); font-size: 80px; color: rgba(255, 0, 0, 0.1); font-weight: bold; z-index: -1; pointer-events: none; border: 5px solid rgba(255, 0, 0, 0.1); padding: 10px 40px; }
				</style>
			</head>
			<body>
				<div class="container">
					<xsl:if test="$IsDraft = 'true'">
						<div class="watermark">BẢN NHÁP</div>
					</xsl:if>

					<div class="form-id">Mẫu số: 04/SS-HĐĐT</div>

					<div class="header">
						<div class="nation">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</div>
						<div class="motto">Độc lập - Tự do - Hạnh phúc</div>
						<div class="title">THÔNG BÁO HÓA ĐƠN ĐIỆN TỬ CÓ SAI SÓT</div>
					</div>

					<div style="text-align: center; margin-bottom: 15px;">
						<strong>Kính gửi:</strong>
						<xsl:value-of select="//DLieu/TBao/TCQT"/>
					</div>

					<table class="info-table">
						<tr>
							<td style="width: 150px; font-weight: bold;">Tên người nộp thuế:</td>
							<td>
								<xsl:value-of select="$CompanyName"/>
							</td>
						</tr>
						<tr>
							<td style="font-weight: bold;">Mã số thuế:</td>
							<td>
								<xsl:value-of select="//DLieu/TBao/DLTBao/MST"/>
							</td>
						</tr>
						<tr>
							<td style="font-weight: bold;">Địa danh:</td>
							<td>
								<xsl:value-of select="//DLieu/TBao/DLTBao/DDanh"/>
							</td>
						</tr>
					</table>

					<div style="margin-bottom: 5px;">Người nộp thuế thông báo về việc hóa đơn điện tử có sai sót như sau:</div>

					<table class="data-table">
						<thead>
							<tr>
								<th rowspan="2" style="width: 40px;">STT</th>
								<th rowspan="2">Mã CQT cấp</th>
								<th rowspan="2">Ký hiệu mẫu</th>
								<th rowspan="2">Ký hiệu HĐ</th>
								<th rowspan="2">Số HĐ</th>
								<th rowspan="2" style="width: 80px;">Ngày lập</th>
								<th rowspan="2">Loại áp dụng</th>
								<th colspan="2">Phân loại sai sót</th>
								<th rowspan="2">Lý do</th>
							</tr>
							<tr>
								<th style="font-size: 11px;">Hủy</th>
								<th style="font-size: 11px;">Điều chỉnh/ Thay thế/ Giải trình</th>
							</tr>
						</thead>
						<tbody>
							<xsl:for-each select="//DLieu/TBao/DLTBao/DSHDon/HDon">
								<tr>
									<td>
										<xsl:value-of select="STT"/>
									</td>
									<td>
										<xsl:value-of select="MCCQT"/>
									</td>
									<td>
										<xsl:value-of select="KHMSHDon"/>
									</td>
									<td>
										<xsl:value-of select="KHHDon"/>
									</td>
									<td>
										<xsl:value-of select="SHDon"/>
									</td>
									<td>
										<xsl:if test="string-length(Ngay) >= 10">
											<xsl:value-of select="substring(Ngay, 9, 2)"/>/<xsl:value-of select="substring(Ngay, 6, 2)"/>/<xsl:value-of select="substring(Ngay, 1, 4)"/>
										</xsl:if>
										<xsl:if test="string-length(Ngay) &lt; 10">
											<xsl:value-of select="Ngay"/>
										</xsl:if>
									</td>
									<td>
										<xsl:choose>
											<xsl:when test="LADHDDT='1'">Có mã</xsl:when>
											<xsl:when test="LADHDDT='2'">K.Mã</xsl:when>
											<xsl:otherwise>
												<xsl:value-of select="LADHDDT"/>
											</xsl:otherwise>
										</xsl:choose>
									</td>
									<td>
										<xsl:if test="TCTBao='1'">x</xsl:if>
									</td>
									<td>
										<xsl:if test="TCTBao='2' or TCTBao='3' or TCTBao='4'">x</xsl:if>
									</td>
									<td class="text-left">
										<xsl:value-of select="LDo"/>
									</td>
								</tr>
							</xsl:for-each>
						</tbody>
					</table>

					<div class="footer">
						<div class="footer-col"></div>
						<div class="footer-col">
							<div style="font-style: italic;">
								Ngày <xsl:value-of select="substring(//TTChung/NLap, 9, 2)"/>
								tháng <xsl:value-of select="substring(//TTChung/NLap, 6, 2)"/>
								năm <xsl:value-of select="substring(//TTChung/NLap, 1, 4)"/>
							</div>
							<div style="font-weight: bold;">NGƯỜI NỘP THUẾ</div>
							<div style="font-style: italic; font-size: 12px;">(Ký điện tử)</div>

							<xsl:if test="count(//DSCKS/Signature) > 0">
								<div class="signature-box">
									<strong>Đã ký điện tử bởi</strong><br/>
									<span style="text-transform: uppercase;">
										<xsl:value-of select="$CompanyName"/>
									</span><br/>
									Ngày ký: <xsl:value-of select="substring(//Signature/Object/SignatureProperties/SignatureProperty/SigningTime, 1, 10)"/>
								</div>
							</xsl:if>
						</div>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>