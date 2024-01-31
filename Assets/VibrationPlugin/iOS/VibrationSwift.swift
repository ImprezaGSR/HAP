

import Foundation
import CoreHaptics

@objc public class VibrationSwift : NSObject {
    @available(iOS 13.0, *)
    @objc public static var hapticEngine: CHHapticEngine?

    @available(iOS 13.0, *)
    @objc public static func setupHapticEngine() {
        do {
            hapticEngine = try CHHapticEngine()
            try hapticEngine?.start()
        } catch {
            print("Failed to restart the engine")
        }
    }

    @available(iOS 13.0, *)
    @objc public static func playHapticEngine(intensityValue: Float, sharpnessValue: Float, durationValue: Float, sustainedValue: Float) {
        if hapticEngine == nil {
            setupHapticEngine()
        }

        let intensity = CHHapticEventParameter(parameterID: .hapticIntensity, value: intensityValue)
        let sharpness = CHHapticEventParameter(parameterID: .hapticSharpness, value: sharpnessValue)
        let sustained = CHHapticEventParameter(parameterID: .sustained, value: sustainedValue)

        let event = CHHapticEvent(eventType: .hapticContinuous, parameters: [intensity, sharpness, sustained], relativeTime: 0, duration: TimeInterval(durationValue))

        do {
            let hapticPattern = try CHHapticPattern(events: [event], parameters: [])
            let hapticPlayer = try hapticEngine?.makePlayer(with: hapticPattern)
            try hapticPlayer?.start(atTime: 0)
        } catch {
            print("Failed to play")
        }
    }
}
