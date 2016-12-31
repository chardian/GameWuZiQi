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
	PROTOCOL_ID = 10000
	PROTOCOL_DIC = {}

	SPLITTER = u':'

	ret = ''

	def __init(self):
		self.ret = 'syntax = "proto2";\n\npackage MyProtocol;\n\n'


	def resolve(self,row):
		tempstr,isProtocol,isEnum =  self.getName(row[0],row[1])
		self.ret = self.ret + tempstr
		length = len(row)
		for    i in range(2,length):
			if row[i] == '':
				continue
			else:
				if isEnum:
					parames =self.getEnumParameter(row[i],i)
				else:
					if isProtocol:
						parames = self.getParameter(row[i],i + 1)
					else:
						parames = self.getParameter(row[i],i)				
				if parames != '':
					self.ret = self.ret + parames
		self.ret = self.ret + '}\n\n'
		#print('ret now is', self.ret)


	def getName(self,t, n):
		n = str.strip(n)
		a = ''
		isProtocol = False
		isEnum = False
		if t == self.TYPE_ENTIRY:
			a = 'message ' + n + ' {\n'
		elif t == self.TYPE_REQ:
			isProtocol = True
			self.PROTOCOL_DIC[self.PROTOCOL_ID] = n + "Req" 
			a = 'message ' + n + 'Req {\n\toptional int32 proID = 1[default = '+str(self.PROTOCOL_ID)+'];\n'
			self.PROTOCOL_ID = self.PROTOCOL_ID + 1
		elif t == self.TYPE_ACK:
			isProtocol = True
			self.PROTOCOL_DIC[self.PROTOCOL_ID] = n + "Ack"
			a = 'message ' + n + 'Ack {\n\toptional int32 proID = 1[default = '+str(self.PROTOCOL_ID)+'];\n'
			self.PROTOCOL_ID = self.PROTOCOL_ID + 1
		elif t == self.TYPE_ENUM:
			isEnum = True
			a = 'enum ' + n + ' {\n'
		#print(a,isProtocol)
		return a,isProtocol,isEnum


	def getParameter(self,s, col):
		s = str.strip(s)
		a = ''
		#print('enter getparameter', s,col)
		arr = s.split(self.SPLITTER)
		#arr[0] arr[1] arr[2]
		#required int32 = 1[arr[2]]
		if len(arr) < 2:
			print('fuck you',s)
		else:
			index = str(col - 1)
			if arr[0].find('[]') > 0:
				#是个数组
				a = '\trepeated ' + arr[0].split('[]')[0]
			else:
				a = '\trequired ' + arr[0]
			if len(arr) >= 3:
				#有默认值
				a = a + ' ' + arr[1] + ' = ' + index + ' [default = ' + arr[2] + '];\n'
			else:
				#required int32 port=3;
				a = a + ' ' + arr[1] + ' = ' + index + ';\n'
			return a
			
	def getEnumParameter(self,s,col):
		s = str.strip(s)
		a = ''
		return '\t' + s + ' = ' + str(col-1) + ';\n'
	
	def output(self):
		print(self.ret)
		print("enum PROTOCOL{")
		for k in self.PROTOCOL_DIC:
			print("\t__" + self.PROTOCOL_DIC[k] + " = " + str(k) + ";")
		print("}\n")

sheet = book.sheet_by_index(0)
p = Parse()
for x in range(2,sheet.nrows):
	p.resolve(sheet.row_values(x))
p.output()