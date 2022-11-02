#include <windows.h>

BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }

    return TRUE;
}

extern "C"
{
    void __declspec(dllexport) __stdcall EnumerateVisibleObjects(unsigned int callback, int filter, unsigned int parPtr)
    {
        typedef void __fastcall func(unsigned int callback, int filter);
        func* function = (func*)parPtr;
        function(callback, filter);
    }


    void __declspec(dllexport) __stdcall LuaCall(char* code, unsigned int ptr)
    {
        typedef void __fastcall func(char* code, const char* unused);
        func* f = (func*)ptr;
        f(code, "Unused");
        return;
    }

    unsigned int __declspec(dllexport) __stdcall FindSlotBySpellId(int SpellId, bool isPet, unsigned int parPtr)
    {
        typedef unsigned int __fastcall func(int SpellId, bool isPet);
        func* f = (func*)parPtr;
        return f(SpellId, isPet);
    }

}