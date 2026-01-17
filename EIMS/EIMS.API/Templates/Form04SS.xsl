<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" encoding="utf-8"/>

	<xsl:param name="CompanyName" select="''" />
	<xsl:param name="Place" select="''" />
	<xsl:param name="IsDraft" select="'false'"/>
	<xsl:param name="ProviderName" select="''" />
	<xsl:param name="IsApproved" select="'false'"/>
	<xsl:template match="/">
		<html>
			<head>
				<title>Mẫu 04/SS-HĐĐT</title>
				<style>
					body { font-family: "Times New Roman", Times, serif; font-size: 13pt; margin: 20px; line-height: 1.3; color: #000; }
					.container { max-width: 800px; margin: 0 auto; position: relative; }

					/* Header Section */
					.top-info { font-size: 11pt; margin-bottom: 20px; }
					.header { text-align: center; margin-bottom: 20px; }
					.nation { font-weight: bold; font-size: 13pt; }
					.motto { font-weight: bold; text-decoration: underline; font-size: 14pt; margin-bottom: 10px; }
					.form-id { text-align: right; font-weight: bold; font-style: italic; font-size: 12pt; margin-bottom: 5px; }
					.title { font-weight: bold; font-size: 16pt; margin: 15px 0; text-transform: uppercase; }

					/* Info Section */
					.info-table { width: 100%; border: none; margin-bottom: 15px; }
					.info-table td { border: none; padding: 5px 0; vertical-align: top; }
					.label-col { font-weight: bold; width: 160px; }

					/* Data Table - Giống PDF */
					.data-table { width: 100%; border-collapse: collapse; margin-top: 10px; }
					.data-table th, .data-table td { border: 1px solid black; padding: 5px; text-align: center; vertical-align: middle; font-size: 12pt; word-wrap: break-word;}
					.data-table th { font-weight: bold; }
					.text-left { text-align: left !important; }
					.col-idx { font-style: italic; font-weight: normal; }
					.signature-section {
						margin-top: 25px;
						display: flex;
						justify-content: space-between; 
						page-break-inside: avoid;       
					}

					.signature-col {
						width: 45%;
						text-align: center;
					}
					/* Footer */
					.footer { margin-top: 30px; display: flex; justify-content: space-between; }
					.footer-col { width: 45%; text-align: center; }
					.signature-box { margin-top: 10px; padding: 5px; border: 2px dashed #0056b3; color: #0056b3; display: inline-block; font-size: 12px; }
					.stamp-box {
					position: absolute;
					top: 120px;           /* Căn chỉnh vị trí dọc tùy ý */
					right: 50px;          /* Căn chỉnh vị trí ngang tùy ý */
					border: 3px solid red; /* Viền đỏ đậm */
					color: red;            /* Chữ đỏ */
					padding: 10px 20px;
					font-weight: bold;
					font-size: 18px;
					text-transform: uppercase;
					font-family: Arial, sans-serif;
					border-radius: 4px;
					transform: rotate(-10deg); /* Xoay nghiêng 10 độ cho giống đóng dấu */
					opacity: 0.8;
					z-index: 100;
					background-color: rgba(255, 255, 255, 0.8); /* Nền trắng mờ để đè lên chữ dưới nếu cần */
					}
					.watermark { position: absolute; top: 40%; left: 50%; transform: translate(-50%, -50%) rotate(-45deg); font-size: 80px; color: rgba(255, 0, 0, 0.1); font-weight: bold; z-index: -1; pointer-events: none; border: 5px solid rgba(255, 0, 0, 0.1); padding: 10px 40px; }
				</style>
			</head>
			<body>
				<div class="page-container" style="position: relative;">
					<xsl:if test="$IsApproved = 'true'">
						<div class="stamp-box">
							THUẾ ĐÃ DUYỆT
						</div>
					</xsl:if>

				</div>
				<div class="container">
					<xsl:if test="$IsDraft = 'true'">
						<div class="watermark">BẢN NHÁP</div>
					</xsl:if>

					<div class="top-info">
						<div style="float: right;" class="form-id">Mẫu số: 04/SS-HĐĐT</div>
						<div>Đơn vị cung cấp dịch vụ hóa đơn điện tử:</div>
						<div style="font-weight: bold;">
							<xsl:value-of select="$ProviderName"/>
						</div>
						<div style="clear: both;"></div>
					</div>

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
							<td class="label-col">Tên người nộp thuế:</td>
							<td>
								<xsl:value-of select="$CompanyName"/>
							</td>
						</tr>
						<tr>
							<td class="label-col">Mã số thuế:</td>
							<td>
								<xsl:value-of select="//DLieu/TBao/DLTBao/MST"/>
							</td>
						</tr>
					</table>

					<div style="margin-bottom: 10px;">Người nộp thuế thông báo về việc hóa đơn điện tử có sai sót như sau:</div>

					<table style="width: 100%; border-collapse: collapse; border: 1px solid black; table-layout: fixed; font-size: 13px;">
    
    <colgroup>
        <col style="width: 5%;" />  <col style="width: 11%;" /> <col style="width: 6%;" />  <col style="width: 8%;" />  <col style="width: 7%;" />  <col style="width: 10%;" /> <col style="width: 17%;" /> <col style="width: 10%;" /> <col style="width: 26%;" /> </colgroup>

    <thead>
        <tr style="background-color: #f0f0f0;"> <th style="border: 1px solid black; padding: 5px; font-weight: bold;">STT</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Mã CQT cấp</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Ký hiệu<br/>mẫu số</th> <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Ký hiệu<br/>hóa đơn</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Số<br/>hóa đơn</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Ngày lập<br/>hóa đơn</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Loại hóa đơn điện tử</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Phân loại</th>
            <th style="border: 1px solid black; padding: 5px; font-weight: bold;">Lý do</th>
        </tr>
        <tr class="col-idx">
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(1)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(2)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(3)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(4)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(5)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(6)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(7)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(8)</td>
            <td style="border: 1px solid black; text-align: center; font-style: italic;">(9)</td>
        </tr>
    </thead>

    <tbody>
        <xsl:for-each select="//DLieu/TBao/DLTBao/DSHDon/HDon">
            <tr>
                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top;">
                    <xsl:value-of select="STT"/>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top; word-break: break-all; font-size: 11px;">
                    <xsl:value-of select="MCCQT"/>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top;">
                    <xsl:value-of select="KHMSHDon"/>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top; white-space: nowrap;">
                    <xsl:value-of select="KHHDon"/>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top; white-space: nowrap;">
                    <xsl:value-of select="SHDon"/>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top; white-space: nowrap;">
                    <xsl:if test="string-length(Ngay) >= 10">
                        <xsl:value-of select="substring(Ngay, 9, 2)"/>/<xsl:value-of select="substring(Ngay, 6, 2)"/>/<xsl:value-of select="substring(Ngay, 1, 4)"/>
                    </xsl:if>
                    <xsl:if test="string-length(Ngay) &lt; 10">
                        <xsl:value-of select="Ngay"/>
                    </xsl:if>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: left; vertical-align: top; word-wrap: break-word;">
                     <xsl:choose>
                        <xsl:when test="LADHDDT='1'">HĐĐT theo NĐ 123/2020</xsl:when> <xsl:when test="LADHDDT='2'">HĐ có mã xác thực</xsl:when>
                        <xsl:otherwise><xsl:value-of select="LADHDDT"/></xsl:otherwise>
                    </xsl:choose>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: center; vertical-align: top;">
                     <xsl:choose>
                        <xsl:when test="TCTBao='1'">Thông báo</xsl:when>
                        <xsl:when test="TCTBao='2'">Điều chỉnh</xsl:when>
                        <xsl:when test="TCTBao='3'">Thay thế</xsl:when>
                        <xsl:when test="TCTBao='4'">Giải trình</xsl:when>
                    </xsl:choose>
                </td>

                <td style="border: 1px solid black; padding: 3px 5px; text-align: left; vertical-align: top; word-wrap: break-word;">
                    <xsl:value-of select="LDo"/>
                </td>
            </tr>
        </xsl:for-each>
    </tbody>
</table>

					<div class="signature-section">
                        <div class="signature-col"></div>

                        <div class="signature-col">
                            <div style="font-style: italic; margin-bottom: 5px;">
                                <xsl:value-of select="$Place"/>, 
                                ngày <xsl:value-of select="substring(//TTChung/NLap, 9, 2)"/>
                                tháng <xsl:value-of select="substring(//TTChung/NLap, 6, 2)"/>
                                năm <xsl:value-of select="substring(//TTChung/NLap, 1, 4)"/>
                            </div>

                            <div style="font-weight: bold; text-transform: uppercase;">NGƯỜI NỘP THUẾ</div>
                            <div style="font-style: italic; font-size: 13px; margin-bottom: 5px;">(Ký, ghi rõ họ tên)</div>

                            <xsl:choose>
                                <xsl:when test="count(//*[local-name()='Signature']) > 0">
                                    <div class="signature-box">
                                        <div style="font-weight: bold; font-size: 11px;">Signature Valid</div>
                    
                                        <div style="margin-top: 5px;">
                                            <strong>Ký bởi: </strong>
                                            <span style="text-transform: uppercase; font-weight: bold;">
                                                <xsl:value-of select="$CompanyName"/>
                                            </span>
                                        </div>
                    
                                        <div style="font-size: 11px; margin-top: 3px;">
                                            <strong>Ngày ký: </strong>
                                            <xsl:choose>
                                                <xsl:when test="string-length((//*[local-name()='SigningTime'])[1]) > 0">
                                                    <xsl:variable name="sTime" select="(//*[local-name()='SigningTime'])[1]"/>
                                                    <xsl:value-of select="substring($sTime, 9, 2)"/>/<xsl:value-of select="substring($sTime, 6, 2)"/>/<xsl:value-of select="substring($sTime, 1, 4)"/>
                                                    <xsl:text> </xsl:text>
                                                    <xsl:value-of select="substring($sTime, 12, 8)"/>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:value-of select="substring(//TTChung/NLap, 9, 2)"/>/<xsl:value-of select="substring(//TTChung/NLap, 6, 2)"/>/<xsl:value-of select="substring(//TTChung/NLap, 1, 4)"/>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </div>
                    
                                        <div class="check-mark">✔</div>
                                        <div style="clear: both;"></div>
                                    </div>
                                </xsl:when>
            
                                <xsl:otherwise>
                                    <div style="margin-top: 60px; color: #999; font-style: italic;">
                                        (Chưa ký điện tử)
                                    </div>
                                </xsl:otherwise>
                            </xsl:choose>
                        </div>
                    </div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>