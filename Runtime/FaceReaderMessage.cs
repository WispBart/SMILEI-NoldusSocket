using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//     server message received as: {
// "time": "00:00:05.300",
// "Neutral": 0.0737,
// "Happy": 0.8548,
// "Sad": 0.0004,
// "Angry": 0.0091,
// "Surprised": 0.0006,
// "Scared": 0.0008,
// "Disgusted": 0.0005,
// "Valence": 0.8457,
// "Arousal": 0.7917,
// "Quality": 0.8797,
// "Heart Rate": "Unknown",
// "Heart Rate Variability": "Unknown",
// "Heart Rate Warnings": "Frame rate too low"
// }

public struct FaceReaderMessage
{
    public TimeSpan time;
    public float Neutral;
    public float Happy;
    public float Sad;
    public float Angry;
    public float Surprised;
    public float Scared;
    public float Disgusted;
    public float Valence;
    public float Arousal;
    public float Quality;

    public static FaceReaderMessage FromJson(string json)
    {
        FaceReaderMessage newMessage = new FaceReaderMessage();
        newMessage = JsonUtility.FromJson<FaceReaderMessage>(json);
        return newMessage;
    }
}
