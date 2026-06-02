Customization of Simple Toon Shader for mobile. 

### 🚀 Mobile Performance & Optimization Improvements

* **70-80% Shader Variant Reduction:** Stripped the standard URP `ForwardLit` pass keywords down to only 6 critical lighting keywords. This drastically minimizes shader compilation overhead, slashes build sizes, and mitigates runtime loading hitches on mobile devices.
* **Full SRP Batcher Compatibility:** Wrapped all material properties cleanly within a single `CBUFFER_START(UnityPerMaterial)` block. This allows the CPU to upload material data once and render multiple objects sharing this shader in a single unified draw call.
* **Optimized Inverted-Hull Outline Pass:** * Isolated outline logic into a dedicated pass tagged with `"LightMode" = "Outline"` for seamless SRP batching.
  * Stripped all expensive lighting calculations from the outline fragment shader, executing flat color rendering instead.
  * Offloaded outline expansion math entirely to the vertex shader using `tex2Dlod` to sample the Z-offset mask safely at Mip Level 0.
* **Eliminated Shadow Fragment Overhead:** Removed unnecessary fragment shader logic from the `ShadowCaster` pass for non-alpha-clipped materials, speeding up shadow map rendering passes by an estimated 15-20%.
* **Zero Custom URP Overhead:** Bypassed Unity's standard, heavy `LitInput.hlsl` entirely. Rewrote custom, compact data structures (`ToonSurfaceData` and `ToonLightingData`) to pass only the absolute necessary parameters between the vertex and fragment stages.
* **Compile-Time Static Branching:** Replaced performance-heavy dynamic `if` statements in the fragment shader with compile-time toggle properties (e.g., `_UseEmission`, `_UseOcclusion`), ensuring the mobile GPU doesn't compute data for unused material features.

* <img width="761" height="510" alt="example" src="https://github.com/user-attachments/assets/48074b31-900c-4db4-ad54-50b8de22c97c" />
