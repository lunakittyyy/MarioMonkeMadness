#if DEBUG
using LibSM64;
using MarioMonkeMadness.Components;
using UnityEngine;

namespace MarioMonkeMadness.Behaviours
{
    public class MarioDebugWindow : MonoBehaviour
    {
        public SM64Mario MyMario;
        internal MarioWaterDetector MyMarioWaterDetector;
        internal RealtimeTerrainManager MyRealtime; 
        public Rect MarioDebugGUI = new Rect(20, 100, 300, 500);
        void OnGUI()
        {
            MarioDebugGUI = GUI.Window(MyMario.marioId, MarioDebugGUI, DoDebugWindow, $"Mario {MyMario.marioId} Debug");
        }
        
        void DoDebugWindow(int windowID)
        {
            var s = MyMario.marioState;
            GUI.Label(new Rect(0, 15, 10000, 10000), 
                $"========== Mario pos/rot/ang =========\n" +
                    $"SM64 pos: {s.position[0]}, {s.position[1]}, {s.position[2]}\n" +
                    $"Unity pos: {s.unityPosition[0]}, {s.unityPosition[1]}, {s.unityPosition[2]}\n" +
                    $"Vel: {s.velocity[0]}, {s.velocity[1]}, {s.velocity[2]}\n" +
                    $"Forward vel: {s.forwardVel}\n" +
                    $"Face angle: {s.faceAngle}\n" +
                    $"Twirl yaw: {s.twirlYaw}\n" +
                    $"========== Mario state =========\n" +
                    $"Health: {s.health}\n" +
                    $"Action: {s.action:X}\n" +
                    $"Ac. Arg: {s.actionArg}\n" +
                    $"Ac. State: {s.actionState}\n" +
                    $"Ac. Time: {s.actionTimer}\n" +
                    $"Anim ID: {s.animID:X}\n" +
                    $"Anim. frame: {s.animFrame}\n" +
                    $"Anim. timer: {s.animTimer}\n" +
                    $"========== Water ==========\n" +
                    $"Water volumes: {MyMarioWaterDetector.waterVolumes.Count}\n" +
                    $"Water level: {MyMarioWaterDetector.waterLevel}\n" +
                    $"========== Terrain ==========\n" +
                    $"Terrain count: {MyRealtime.terrainList.Count}\n" +
                    $"Terrain ");
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
    }
}
#endif