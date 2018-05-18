#pragma once



#define LWS_DLL_EXPORT __declspec(dllexport)


extern "C"
{
	// Random number generator in HSP's standard library.
	LWS_DLL_EXPORT int HSP_rnd(int max);
	LWS_DLL_EXPORT void HSP_randomize_time();
	LWS_DLL_EXPORT void HSP_randomize(int seed);

	// Random number generator in exrand.dll.
	LWS_DLL_EXPORT int HSPexrand_rnd(int max);
	LWS_DLL_EXPORT void HSPexrand_randomize_time();
	LWS_DLL_EXPORT void HSPexrand_randomize(int seed);
}


#undef LWS_DLL_EXPORT
