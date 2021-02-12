// Assignment2.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Assignment2.h"
#include <cstdlib>

#define MAX_LOADSTRING 100

class Car
{
public:
    int n;
    int x, y;
    Car(int _n, int _x, int _y) : n(_n), x(_x), y(_y)
    {
    }
    void Draw(HDC hdc)
    {
        Ellipse(hdc, x, y, x + 10, y + 10);
        Ellipse(hdc, x + 20, y, x + 30, y + 10);
        Rectangle(hdc, x, y - 10, x + 30, y);
    }
};

class CarList
{
public:
    Car* t[1000];
    int m_i;
    CarList()
    {
        m_i = 0;
    }
    void push(Car* pCar)
    {
        t[m_i++] = pCar;
    }
    void Draw(HDC hdc)
    {
        for (int k = 0; k < m_i; k++)
            t[k]->Draw(hdc); //  same as (*t[m_i]).Draw();
    }
    void MoveX(int dx, int k)
    {
        //for (int k = 0; k < m_i; k++)
            t[k]->x += dx;
    }
    void MoveY(int dy, int k)
    {
        //for (int k = 0; k < m_i; k++)
        t[k]->y += dy;
    }
    void Clear(int k)
    {
        //for (int k = 0; k < m_i; k++)
            delete t[k];
    }
};

CarList carList;
CarList carList2;


// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    MyDlg(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_ASSIGNMENT2, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_ASSIGNMENT2));

    MSG msg;

    // Main message loop:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ASSIGNMENT2));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_ASSIGNMENT2);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

static int pw = 0; //probability for west
static int pn = 0; // probability for north

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE: Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    static int state = 1; //state for north
    static int state2 = 3; //state for west
    static int deleted = 0;
    static int deleted2 = 0;
    static int r = 0;

    switch (message)
    {
    case WM_CREATE:
        SetTimer(hWnd, 0, 3000, 0);
        SetTimer(hWnd, 3, 1000, 0);
        DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, MyDlg);
        break;
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_KEYDOWN:
        if (wParam == VK_LEFT) {
            if(pw < 100)
                pw = pw + 10;
        }
        else if (wParam == VK_RIGHT) {
            if(pw > 0)
                pw = pw - 10;
        }
        else if (wParam == VK_UP) {
            if(pn < 100)
                pn = pn + 10;
        }
        else if (wParam == VK_DOWN) {
            if(pn > 0)
                pn = pn - 10;
        }
        break;
    case WM_LBUTTONDOWN:
        /*if (state < 4)
            state++;
        else
            state = 1;

        if (state2 < 4)
            state2++;
        else
            state2 = 1;*/
        //carList.push(new Car(0, 100, 325));
        //carList2.push(new Car(0, 560, 0));
        SetTimer(hWnd, 1, 100, 0);
        InvalidateRect(hWnd, 0, true);
        break;
    case WM_TIMER:
        switch (wParam)
        {
        case 0:
            if (state < 4)
                state++;
            else
                state = 1;

            if (state2 < 4)
                state2++;
            else
                state2 = 1;
            break;
        case 1:
            for (int i = deleted; i < carList.m_i; i++) {
                if (carList.t[i]->x > 500 && carList.t[i]->x < 525) {
                    if (state2 == 3) {
                        carList.MoveX(10, i);
                    }
                    else {

                    }
                }
                else {
                    if (i != deleted) {
                        if ((carList.t[i - 1]->x - carList.t[i]->x) > 40)
                            carList.MoveX(10, i);
                    }
                    else {
                        carList.MoveX(10, i);
                    } 
                }
                if (carList.t[i]->x > 1000) {
                    carList.Clear(i);
                    deleted++;
                }
            }
            
            for (int i = deleted2; i < carList2.m_i; i++) {
                if (carList2.t[i]->y > 275 && carList2.t[i]->y < 300) {
                    if (state == 3) {
                        carList2.MoveY(10, i);
                    }
                    else {

                    }
                }
                else {
                    if (i != deleted2) {
                        if ((carList2.t[i - 1]->y - carList2.t[i]->y) > 40)
                            carList2.MoveY(10, i);
                    }
                    else {
                        carList2.MoveY(10, i);
                    }
                }
                if (carList2.t[i]->y > 700) {
                    carList2.Clear(i);
                    deleted2++;
                }
            }
            break;
        case 3:
            //use pw and pn in random generator
            r = rand() % 100 + 1;     //in the range 1 to 100
            //add car based on random
            if(r <= pw)
                carList.push(new Car(0, 100, 325));
            if(r <= pn)
                carList2.push(new Car(0, 560, 0));
            break;
        default:
            break;
        }
        
        InvalidateRect(hWnd, 0, true);
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            // TODO: Add any drawing code that uses hdc here...
            HBRUSH hb = (HBRUSH)GetStockObject(BLACK_BRUSH);
            HBRUSH gray = (HBRUSH)GetStockObject(GRAY_BRUSH);
            HBRUSH green = CreateSolidBrush(RGB(0, 255, 0));
            HBRUSH yellow = CreateSolidBrush(RGB(255, 255, 0));
            HBRUSH red = CreateSolidBrush(RGB(255, 0, 0));
            HGDIOBJ hOrg = SelectObject(hdc, hb);
            //Traffic lights background
            Rectangle(hdc, 510, 350, 550, 450);
            Rectangle(hdc, 600, 350, 700, 390);

            //Roads
            SelectObject(hdc, gray);
            Rectangle(hdc, 100, 300, 1000, 350);
            Rectangle(hdc, 550, 0, 600, 700);

            /*if (state == 1 || state == 2) {
                SelectObject(hdc, green);
            }
            else {
                SelectObject(hdc, red);
            }*/
            SelectObject(hdc, green);
            carList.Draw(hdc);
            carList2.Draw(hdc);

            //Lights south
            if (state <= 2) {
                SelectObject(hdc, red);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 515, 445, 545, 420);
            if (state == 2 || state == 4) {
                SelectObject(hdc, yellow);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 515, 415, 545, 390);
            
            if (state == 3) {
                SelectObject(hdc, green);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 515, 385, 545, 360);

            //Lights west
            if (state2 <= 2) {
                SelectObject(hdc, red);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 670, 355, 695, 385);
            if (state2 == 2 || state2 == 4) {
                SelectObject(hdc, yellow);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 640, 355, 665, 385);

            if (state2 == 3) {
                SelectObject(hdc, green);
            }
            else {
                SelectObject(hdc, gray);
            }
            Ellipse(hdc, 610, 355, 635, 385);

            SelectObject(hdc, hOrg);
            DeleteObject(hb);
            DeleteObject(gray);
            DeleteObject(green);
            DeleteObject(yellow);
            DeleteObject(red);
            EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        KillTimer(hWnd, 0);
        KillTimer(hWnd, 1);
        KillTimer(hWnd, 3);
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}

// Message handler for Dialog box.
INT_PTR CALLBACK MyDlg(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
    {
        HWND intPw = GetDlgItem(hDlg, IDC_PW);
        SetWindowText(intPw, L"10");
        HWND intPn = GetDlgItem(hDlg, IDC_PN);
        SetWindowText(intPn, L"10");
        return (INT_PTR)TRUE;
    }

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            BOOL isNum = false;
            pw = GetDlgItemInt(hDlg, IDC_PW, &isNum, true);
            pn = GetDlgItemInt(hDlg, IDC_PN, &isNum, true);
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}
