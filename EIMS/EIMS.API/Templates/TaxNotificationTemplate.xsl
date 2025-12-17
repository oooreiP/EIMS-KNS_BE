<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:tdiep="http://tempuri.org/TDiepSchema.xsd">
    
    <xsl:output method="html" indent="yes" encoding="utf-8"/>

    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Thông báo từ Cơ quan Thuế</title>
                <style>
                    body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: #f9f9f9; padding: 20px; }
                    .notify-card { max-width: 700px; margin: 0 auto; background: #fff; border-radius: 8px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); overflow: hidden; }
                    
                    /* Header Status */
                    .status-header { padding: 20px; text-align: center; color: #fff; }
                    .status-header.success { background: linear-gradient(135deg, #28a745 0%, #218838 100%); }
                    .status-header.error { background: linear-gradient(135deg, #dc3545 0%, #c82333 100%); }
                    .status-icon { font-size: 48px; display: block; margin-bottom: 10px; }
                    .status-title { font-size: 24px; font-weight: bold; margin: 0; text-transform: uppercase; }
                    .status-desc { font-size: 16px; opacity: 0.9; margin-top: 5px; }

                    /* Content Body */
                    .card-body { padding: 30px; }
                    .info-group { margin-bottom: 15px; border-bottom: 1px solid #eee; padding-bottom: 15px; }
                    .info-group:last-child { border-bottom: none; }
                    .info-label { font-weight: bold; color: #555; display: block; margin-bottom: 5px; }
                    .info-value { font-size: 16px; color: #333; }
                    .mccqt-highlight { font-family: 'Courier New', monospace; font-weight: bold; color: #0056b3; font-size: 18px; letter-spacing: 1px; }

                    /* Error List */
                    .error-container { background: #fff5f5; border-left: 5px solid #dc3545; padding: 15px; margin-top: 15px; }
                    .error-title { font-weight: bold; color: #dc3545; margin-bottom: 10px; }
                    .error-item { margin-bottom: 5px; color: #333; }

                    /* Footer */
                    .card-footer { background: #f1f1f1; padding: 15px; text-align: center; font-size: 13px; color: #777; }
                </style>
            </head>
            <body>
                <div class="notify-card">
                    <xsl:apply-templates select="//*[local-name()='TBao']" />
                    
                    <xsl:if test="count(//*[local-name()='TBao']) = 0">
                        <div class="status-header error">
                            <div class="status-title">KHÔNG TÌM THẤY DỮ LIỆU</div>
                            <div class="status-desc">XML không chứa thẻ Thông báo (TBao)</div>
                        </div>
                        <div class="card-body">
                            <pre><xsl:copy-of select="."/></pre>
                        </div>
                    </xsl:if>
                </div>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="*[local-name()='TBao']">
        <xsl:variable name="status" select="*[local-name()='TTTNhan']"/>
        
        <xsl:choose>
            <xsl:when test="$status = 1">
                <div class="status-header error">
                    <span class="status-icon">&#10060;</span>
                    <h1 class="status-title">TỪ CHỐI CẤP MÃ</h1>
                    <div class="status-desc">Hóa đơn điện tử có sai sót nghiệp vụ</div>
                </div>
            </xsl:when>
            <xsl:otherwise>
                <div class="status-header success">
                    <span class="status-icon">&#10004;</span>
                    <h1 class="status-title">CẤP MÃ THÀNH CÔNG</h1>
                    <div class="status-desc"><xsl:value-of select="*[local-name()='MoTa']"/></div>
                </div>
            </xsl:otherwise>
        </xsl:choose>

        <div class="card-body">
            <xsl:if test="$status != 1 and *[local-name()='MCCQT']">
                <div class="info-group">
                    <span class="info-label">Mã Của Cơ Quan Thuế (MCCQT)</span>
                    <span class="info-value mccqt-highlight">
                        <xsl:value-of select="*[local-name()='MCCQT']"/>
                    </span>
                </div>
            </xsl:if>

            <div class="info-group">
                <span class="info-label">Thời gian tiếp nhận</span>
                <span class="info-value">
                    <xsl:value-of select="substring(*[local-name()='NNhan'], 9, 2)"/>/<xsl:value-of select="substring(*[local-name()='NNhan'], 6, 2)"/>/<xsl:value-of select="substring(*[local-name()='NNhan'], 1, 4)"/> 
                    &#160;<xsl:value-of select="substring(*[local-name()='NNhan'], 12, 8)"/>
                </span>
            </div>

            <div class="info-group">
                <span class="info-label">Số thông báo</span>
                <span class="info-value"><xsl:value-of select="*[local-name()='SoTBao']"/></span>
            </div>

            <div class="info-group">
                <span class="info-label">Mã tham chiếu (MTDiep)</span>
                <span class="info-value" style="font-family:monospace"><xsl:value-of select="*[local-name()='MTDiep']"/></span>
            </div>

            <xsl:if test="$status = 1">
                <div class="error-container">
                    <div class="error-title">Chi tiết lỗi:</div>
                    <xsl:for-each select="*[local-name()='LDo']">
                        <div class="error-item">• <xsl:value-of select="."/></div>
                    </xsl:for-each>
                </div>
            </xsl:if>
        </div>

        <div class="card-footer">
            Thông báo này được sinh ra tự động từ hệ thống T-VAN.
        </div>
    </xsl:template>
</xsl:stylesheet>