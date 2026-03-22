using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace SalesInventorySystem.Classes
{
    public class SoundPlayer
    {
        //public static async Task PlayNotificationSoundAsync(string soundFilePath)
        //{
        //    // Use Task.Run to offload the sound loading and playback logic to a background thread.
        //    // This prevents the main UI thread from becoming unresponsive, even if the sound file
        //    // is large or there are delays in accessing it.
        //    await Task.Run(() =>
        //    {
        //        try
        //        {
        //            // 1. Check if the specified sound file actually exists.
        //            if (!File.Exists(soundFilePath))
        //            {
        //                Console.WriteLine($"Error: Sound file not found at '{soundFilePath}'. Please verify the path.");
        //                // Optionally, you could play a default system sound here if the file is missing.
        //                // SystemSounds.Exclamation.Play();
        //                return; // Exit the method if the file is not found.
        //            }

        //            // 2. Create a new SoundPlayer instance with the provided file path.
        //            // The 'using' statement ensures that the SoundPlayer object is properly
        //            // disposed of after it's no longer needed, releasing system resources.
        //            using (SoundPlayer player = new SoundPlayer(soundFilePath))
        //            {
        //                // 3. Load the sound into memory. This operation can be synchronous
        //                // but since it's inside Task.Run, it won't block the main thread.
        //                player.Load();

        //                // 4. Play the sound. The Play() method plays the sound asynchronously
        //                // on an internal thread managed by SoundPlayer, and returns immediately.
        //                player.Play();

        //                //Console.WriteLine($"Notification: Playing sound from '{soundFilePath}'");
        //            }
        //        }
        //        // 5. Implement robust error handling for common issues.
        //        catch (FileNotFoundException)
        //        {
        //            Console.WriteLine($"Error: The sound file was not found at '{soundFilePath}'. Double-check the file path and ensure it's accessible.");
        //        }
        //        catch (InvalidOperationException ex)
        //        {
        //            // This typically occurs if the sound file is not a valid .wav format
        //            // or if there's an issue with the audio device.
        //            Console.WriteLine($"Error playing sound from '{soundFilePath}': {ex.Message}. Ensure the file is a valid .wav format and your audio device is working.");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Catch any other unexpected errors during sound playback.
        //            Console.WriteLine($"An unexpected error occurred while attempting to play sound from '{soundFilePath}': {ex.Message}");
        //        }
        //    });
        //}
    }
}
