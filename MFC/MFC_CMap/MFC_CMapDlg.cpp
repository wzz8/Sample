
// MFC_CMapDlg.cpp: 实现文件
//

#include "pch.h"
#include "framework.h"
#include "MFC_CMap.h"
#include "MFC_CMapDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CMFCCMapDlg 对话框



CMFCCMapDlg::CMFCCMapDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_MFC_CMAP_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCCMapDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CMFCCMapDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, &CMFCCMapDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CMFCCMapDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CMFCCMapDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CMFCCMapDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON5, &CMFCCMapDlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON6, &CMFCCMapDlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_BUTTON7, &CMFCCMapDlg::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON8, &CMFCCMapDlg::OnBnClickedButton8)
	ON_BN_CLICKED(IDC_BUTTON9, &CMFCCMapDlg::OnBnClickedButton9)
	ON_BN_CLICKED(IDC_BUTTON10, &CMFCCMapDlg::OnBnClickedButton10)
	ON_BN_CLICKED(IDC_BUTTON11, &CMFCCMapDlg::OnBnClickedButton11)
	ON_BN_CLICKED(IDC_BUTTON12, &CMFCCMapDlg::OnBnClickedButton12)
	ON_BN_CLICKED(IDC_BUTTON13, &CMFCCMapDlg::OnBnClickedButton13)
	ON_BN_CLICKED(IDC_BUTTON14, &CMFCCMapDlg::OnBnClickedButton14)
	ON_BN_CLICKED(IDC_BUTTON15, &CMFCCMapDlg::OnBnClickedButton15)
	ON_BN_CLICKED(IDC_BUTTON16, &CMFCCMapDlg::OnBnClickedButton16)
	ON_BN_CLICKED(IDC_BUTTON17, &CMFCCMapDlg::OnBnClickedButton17)
END_MESSAGE_MAP()


// CMFCCMapDlg 消息处理程序

BOOL CMFCCMapDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != nullptr)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CMFCCMapDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CMFCCMapDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CMFCCMapDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

static CMap<int, float, int, float> intFloatMap;

void CMFCCMapDlg::OnBnClickedButton1()
{
	// SetAt(arg_key:float, arg_value:float)
	intFloatMap.SetAt(1, 2);

	intFloatMap[1] = 1;
	intFloatMap[2] = 2;

	int one = intFloatMap[1];
}


void CMFCCMapDlg::OnBnClickedButton2()
{
	//Specifies the memory-allocation granularity for extending the map.
	CMap<int, int, int, int> cMap(10);
}


void CMFCCMapDlg::OnBnClickedButton3()
{
	CMap<int, int, int, int> cMap(10);
	cMap.SetAt(1, 1);
	int count = cMap.GetCount();
}


void CMFCCMapDlg::OnBnClickedButton4()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int count = cMap.GetHashTableSize();
}


void CMFCCMapDlg::OnBnClickedButton5()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}
	POSITION pos = cMap.GetStartPosition();
	int key; int value;
	
	while (pos != NULL) {
		cMap.GetNextAssoc(pos, key, value);
		TRACE(_T("\t[%d] = (%d)\n"), key, value);
	}
}


void CMFCCMapDlg::OnBnClickedButton6()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int size = cMap.GetSize();
}


void CMFCCMapDlg::OnBnClickedButton7()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}
	POSITION pos = cMap.GetStartPosition();
	int key; int value;

	while (pos != NULL) {
		cMap.GetNextAssoc(pos, key, value);
		TRACE(_T("\t[%d] = (%d)\n"), key, value);
	}
}


void CMFCCMapDlg::OnBnClickedButton8()
{
	CMap<int, int, int, int> cMap(100);

	int hashTableSize = cMap.GetHashTableSize();
	cMap.InitHashTable(20);
	hashTableSize = cMap.GetHashTableSize();
}


void CMFCCMapDlg::OnBnClickedButton9()
{
	CMap<int, int, int, int> cMap(100);

	bool isEmpty = cMap.IsEmpty();

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	isEmpty = cMap.IsEmpty();
}


void CMFCCMapDlg::OnBnClickedButton10()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int value;
	bool isFind = cMap.Lookup(1, value);
	isFind = cMap.Lookup(-1, value);
}


void CMFCCMapDlg::OnBnClickedButton11()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int key = cMap.PGetFirstAssoc()->key;
	int val = cMap.PGetFirstAssoc()->value;
}


void CMFCCMapDlg::OnBnClickedButton12()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	CMap<int, int, int, int>::CPair* pCPair = cMap.PGetFirstAssoc();
	pCPair = cMap.PGetNextAssoc(pCPair);

	int key = pCPair->key;
	int value = pCPair->value;
}


void CMFCCMapDlg::OnBnClickedButton13()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	auto pPair = cMap.PLookup(1);
	int key = pPair->key;
	int value = pPair->value;

	pPair = cMap.PLookup(-1);
}


void CMFCCMapDlg::OnBnClickedButton14()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int count = cMap.GetCount();

	cMap.RemoveAll();

	count = cMap.GetCount();
}


void CMFCCMapDlg::OnBnClickedButton15()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}

	int count = cMap.GetCount();

	cMap.RemoveKey(1);

	count = cMap.GetCount();
}


void CMFCCMapDlg::OnBnClickedButton16()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap.SetAt(i, i);
	}
}


void CMFCCMapDlg::OnBnClickedButton17()
{
	CMap<int, int, int, int> cMap(100);

	for (size_t i = 0; i < 10000; i++)
	{
		cMap[i] = i;
	}
}
