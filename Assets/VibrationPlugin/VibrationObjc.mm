#import <Foundation/Foundation.h>
#import <CoreHaptics/CoreHaptics.h>
#import <UnityFramework-Swift.h>

#ifdef __cplusplus
extern "C" {
#endif
    void setupHapticEngine() {
        [VibrationSwift setupHapticEngine];
    }

    void playHapticEngine(float intensity, float sharpness, float duration, float sustained) {
        [VibrationSwift playHapticEngineWithIntensityValue:intensity sharpnessValue:sharpness durationValue:duration sustainedValue: sustained];
    }

#ifdef __cplusplus
}
#endif