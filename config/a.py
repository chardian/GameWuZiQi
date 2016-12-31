# -*- coding:utf-8 -*-
import xlrd

book = xlrd.open_workbook('config.xlsx')

class Parse:
    COL_TYPE = 0
    COL_NAME = 1

    TYPE_ENTIRY = 0
    TYPE_REQ = 1
    TYPE_ACK = 2
    TYPE_ENUM = 3

    SPLITTER = u':'

    ret = ''

    def __init(self):
        self.ret = 'syntax = "proto2";\n\npackage MyProtocol;\n\n'
      

    def resolve(self,row):
        self.ret = self.ret + self.getName(row[0],row[1])
        length = len(row)
        for    i in range(2,length):
            if row[i] == '':
            continue
            else:
            parames = self.getParameter(row[i],i)
            if parames != '':
                self.ret = self.ret + parames
        self.ret = self.ret + '}\n\n'
        #print('ret now is', self.ret)


   def getName(self,t, n):
      a = ''
      if t == self.TYPE_ENTIRY:
         a = 'message ' + n + ' {\n'
      elif t == self.TYPE_REQ:
         a = 'message ' + n + 'Req {\n'
      elif t == self.TYPE_ACK:
         a = 'message ' + n + 'Ack {\n'
      elif t == self.TYPE_ENUM:
         a = 'enum ' + n + ' {\n'
      return a


   def getParameter(self,s, col):
      a = ''
      #print('enter getparameter', s,col)
      arr = s.split(self.SPLITTER)
      #arr[0] arr[1] arr[2]
      #required int32 = 1[arr[2]]
      if len(arr) < 2:
         print('数据错误',s)
      else:
         index = str(col - 1)
         if arr[0].find('[]') > 0:
            #是个数组
            a = 'repeated ' + arr[0].split('[]')[0]
         else:
            a = 'required ' + arr[0]
         if len(arr) >= 3:
            #有默认值
            a = a + ' ' + arr[1] + ' = ' + index + ' [' + arr[2] + '];\n'
         else:
            #required int32 port=3;
            a = a + ' ' + arr[1] + ' = ' + index + ';\n'
      return a



sheet = book.sheet_by_index(0)
p = Parse()
for x in range(2,sheet.nrows):
   p.resolve(sheet.row_values(x))
   #for y in range(sheet.ncols):
   #  cell = sheet.row_values(x)[y]
   #  print(x,y,cell)
print(p.ret)