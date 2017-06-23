import clr
import math
clr.AddReference("System.Drawing")
from System.Drawing import Size
clr.AddReference("System.Windows.Forms")
from System.Windows.Forms import *

def fToDo(sender,e):
        frm = sender.Tag
        frm.richTextBox1.Clear()
        a = int(frm.textBox1.Text)
        b = int(frm.textBox2.Text)
        c = int(frm.textBox3.Text)
        d = int(frm.textBox4.Text)
        netNum = int(frm.textBox5.Text)
        subnets = int(frm.comboBox1.Text)
        netNumExtend = int(math.ceil(math.log(subnets, 2)))
        ip = IP(a,b,c,d,(netNum + netNumExtend))  
        rez = ip.binarize(a,b,c,d)
        podmreze = ip.subNet(ip, netNum, netNumExtend, subnets)
        for i in range(0, subnets):
            if netNum == 24:
                rez[3] = str(int(podmreze[subnets-1-i], 2))
                rez[3]=int(rez[3])
                frm.richTextBox1.Text += str(i + 1) + ". " + str(rez[0])+"."+str(rez[1])+"." +str(rez[2])+"."+str(rez[3]) + "\n"
            if netNum == 16:
                rez[3] = str(int(podmreze[subnets-1-i][8:], 2))
                rez[3]=int(rez[3])
                rez[2] = str(int(podmreze[subnets-1-i][:8], 2))
                frm.richTextBox1.Text += str(i + 1) + ". " + str(rez[0])+"."+str(rez[1])+"." +str(rez[2])+"."+str(rez[3]) + "\n"
            if netNum == 8:
                rez[3] = str(int(podmreze[subnets-1-i][16:], 2))
                rez[3]=int(rez[3])
                rez[2] = str(int(podmreze[subnets-1-i][8:16], 2))
                rez[1] = str(int(podmreze[subnets-1-i][:8], 2))
                frm.richTextBox1.Text += str(i + 1) + ". " + str(rez[0])+"."+str(rez[1])+"." +str(rez[2])+"."+str(rez[3]) + "\n"
def get(frm):
    newToolStrip = ToolStripMenuItem(Text = 'Subnet', Name = 'fToDo', Size = Size(104, 20))
    newToolStrip.Tag = frm
    newToolStrip.Click += fToDo
    frm.pythonFunctionsToolStripMenuItem.DropDownItems.Add(newToolStrip)

class IP:
    a = 0
    b = 0
    c = 0
    d = 0
    md = 0
    def __init__(self, a, b, c, d, md):
        self.a = a
        self.b = b
        self.c = c
        self.d = d
        self.md = md
    def binarize(self,a,b,c,d):
        self.a = bin(a)[2:].zfill(8)
        self.b = bin(b)[2:].zfill(8)
        self.c = bin(c)[2:].zfill(8)
        self.d = bin(d)[2:].zfill(8)
        rez = [a, b, c, d]
        return rez
    def subNet(self, ip, netNum, ex, subnet):
        podmreze = ()
        if int(netNum) == 24:
            nula = '0'
            br0 = 8 - ex
            for i in range(0, br0 - 1):
                nula += '0'
            x = subnet
            for nesto in range(0, subnet):
                if x > 0:    
                    iteracija = int(x - 1)
                    iteracija = bin(iteracija)[2:].zfill(ex)
                    iteracija += nula
                    x = x - 1
                    podmreze += (iteracija,)
        if int(netNum) == 16:
            nula = '0'
            br0 = 16 - ex
            for i in range(0, br0 - 1):
                nula += '0'
            x = subnet
            for nesto in range(0, subnet):
                if x > 0:    
                    iteracija = int(x - 1)
                    iteracija = bin(iteracija)[2:].zfill(ex)
                    iteracija += nula
                    x = x - 1
                    podmreze += (iteracija,)
        if int(netNum) == 8:
            nula = '0'
            br0 = 24 - ex
            for i in range(0, br0 - 1):
                nula += '0'
            x = subnet
            for nesto in range(0, subnet):
                if x > 0:    
                    iteracija = int(x - 1)
                    iteracija = bin(iteracija)[2:].zfill(ex)
                    iteracija += nula
                    x = x - 1
                    podmreze += (iteracija,)    
        return podmreze