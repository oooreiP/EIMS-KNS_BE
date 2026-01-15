xml version=1.0 encoding=utf-8
xslstylesheet version=1.0 xmlnsxsl=httpwww.w3.org1999XSLTransform
  xsloutput method=html indent=yes encoding=utf-8

  xsltemplate match=
    html
      head
        meta charset=UTF-8
        titleThông báo kết quả 04SStitle
        style
          body { font-family 'Segoe UI', sans-serif; background #f5f7fa; padding 20px; }
          .card { max-width 800px; margin 0 auto; background #fff; box-shadow 0 2px 10px rgba(0,0,0,0.1); border-radius 8px; overflow hidden; }
          
           Header 
          .header { background #0056b3; color white; padding 20px; text-align center; }
          .header h2 { margin 0; font-size 22px; text-transform uppercase; }
          .header p { margin 5px 0 0; opacity 0.9; font-size 14px; }

           Info Section 
          .info-section { padding 20px; display grid; grid-template-columns 1fr 1fr; gap 15px; border-bottom 1px solid #eee; }
          .info-item label { display block; font-weight bold; color #666; font-size 12px; }
          .info-item span { display block; font-weight 500; color #333; }

           Table 
          .table-container { padding 20px; overflow-x auto; }
          table { width 100%; border-collapse collapse; font-size 14px; }
          th { background #f8f9fa; padding 12px; text-align left; border-bottom 2px solid #dee2e6; color #495057; }
          td { padding 12px; border-bottom 1px solid #dee2e6; vertical-align top; }
          
           Status Badges 
          .badge { padding 4px 8px; border-radius 4px; font-size 11px; font-weight bold; }
          .badge-success { background #d4edda; color #155724; }
          .badge-error { background #f8d7da; color #721c24; }
          
          .error-text { color #dc3545; font-style italic; font-size 13px; margin-top 4px; display block;}
        style
      head
      body
        xslapply-templates select=[local-name()='DLTBao'] 
      body
    html
  xsltemplate

  xsltemplate match=[local-name()='DLTBao']
    div class=card
      div class=header
        h2xslvalue-of select=[local-name()='Ten']h2
        pMẫu số xslvalue-of select=[local-name()='MSo']p
      div

      div class=info-section
        div class=info-item
          labelSố thông báo CQTlabel
          spanxslvalue-of select=....[local-name()='STBao'][local-name()='So']span
        div
        div class=info-item
          labelNgày thông báolabel
          span
             xslvalue-of select=substring([local-name()='TGNhan'], 9, 2)xslvalue-of select=substring([local-name()='TGNhan'], 6, 2)xslvalue-of select=substring([local-name()='TGNhan'], 1, 4)
          span
        div
        div class=info-item
          labelCơ quan thuếlabel
          spanxslvalue-of select=[local-name()='TCQT']span
        div
        div class=info-item
          labelĐơn vị nhậnlabel
          spanxslvalue-of select=[local-name()='TNNT'] (MST xslvalue-of select=[local-name()='MST'])span
        div
      div

      div class=table-container
        table
          thead
            tr
              thSTTth
              thKý hiệu mẫuth
              thKý hiệuth
              thSố HĐth
              thNgày lậpth
              thKết quả xử lýth
            tr
          thead
          tbody
            xslfor-each select=[local-name()='DSHDon'][local-name()='HDon']
              tr
                tdxslvalue-of select=[local-name()='STT']td
                tdxslvalue-of select=[local-name()='KHMSHDon']td
                tdxslvalue-of select=[local-name()='KHHDon']td
                tdxslvalue-of select=[local-name()='SHDon']td
                td
                   xslvalue-of select=substring([local-name()='NLap'], 9, 2)xslvalue-of select=substring([local-name()='NLap'], 6, 2)xslvalue-of select=substring([local-name()='NLap'], 1, 4)
                td
                td
                  xslchoose
                    xslwhen test=[local-name()='TTTNCCQT'] = 1
                        span class=badge badge-successTiếp nhậnspan
                    xslwhen
                    xslotherwise
                        span class=badge badge-errorKhông tiếp nhậnspan
                        xslif test=[local-name()='DSLDKTNhan'][local-name()='LDo']
                            span class=error-text
                                Lý do xslvalue-of select=[local-name()='DSLDKTNhan'][local-name()='LDo'][local-name()='MTLoi']
                            span
                        xslif
                    xslotherwise
                  xslchoose
                td
              tr
            xslfor-each
          tbody
        table
      div
    div
  xsltemplate
xslstylesheet