using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Streams;
using CSCore.XAudio2;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Security.Policy;

namespace VaporGridCrossPlatform
{
    
    public enum RhythmState { Offbeat, Quarter, Sixteenth}
    public class SongPlayer
    {
        CSCore.XAudio2.XAudio2 Xaudio;
        CSCore.XAudio2.XAudio2MasteringVoice master;
        CSCore.XAudio2.XAudio2SourceVoice songSource;
        CSCore.XAudio2.XAudio2Buffer songBuffer;
        CSCore.WaveFormat waveFmt;
        public double SampleCount { get; private set; }

        double sampleRate;
        double prevSampleCount;
        double quarterInputBuffer;
        double sixteenthInputBuffer;
        double quarterIntervalInSamples;
        double sixteenthIntervalInSamples;
        double bpm;
        double offset;
        double totalOffset;

        float inputBufScale;
        float sixteenthInputBufScale;

        int quarterNotesPassed;
        int sixteenthNotesPassed;
        public int Transpose;

        public bool hasHitQuarterBeat;
        public bool hasHitSixteenthBeat;

        public RhythmState CurrentRhythmState { get { return getRhythmState(); } }

        string filePath = "Music/";
        List<String> songs;
        Random rnd;

        private System.Timers.Timer updateTimer;

        byte[] audioData;
        VoiceState curState;

        public SongPlayer()
        {
            rnd = new Random();
            songs = new List<string>();
            songs.Add("SynthwaveEighteen 73bpm 9.wav");
            songs.Add("SynthwaveEleven 92bpm 0.wav");
            songs.Add("SynthwaveFifteen 87bpm 5.wav");
            songs.Add("SynthwaveFourteen 78bpm 6.wav");
            songs.Add("SynthwaveNineteen 82bpm 6.wav");
            songs.Add("SynthwaveTwo 82bpm 10.wav");
            updateTimer = new System.Timers.Timer();
            updateTimer.Interval = .00000000000000000001;
            updateTimer.Elapsed += OnTimedEvent;
            updateTimer.AutoReset = true;
            sampleRate = 44100;
            inputBufScale = .18f;
            sixteenthInputBufScale = .22f;
            SetupMusic();
        }

        public void SetVolume(float volume)
        {
            master.Volume = volume;
        }

        #region MusicSetup

        private string getSongToPlay()
        {
            return songs[rnd.Next(0, songs.Count)];
        }

        public void SetupMusic()
        {
            string songname = getSongToPlay();
            audioData = getFileData(songname);
            bpm = findBpm(songname);
            Transpose = findTranspose(songname);
            Xaudio = XAudio2.CreateXAudio2();
            master = Xaudio.CreateMasteringVoice();
            waveFmt = new WaveFormat();
            songSource = Xaudio.CreateSourceVoice(waveFmt);
            songBuffer = new XAudio2Buffer(audioData.Length);
            setBufferPtr();
            songSource.SubmitSourceBuffer(songBuffer);
            curState = songSource.GetState();
            setBeatAndBuffer();
            offset = quarterIntervalInSamples - Math.Truncate(quarterIntervalInSamples);
        }

        private int findBpm(string filename)
        {
            //split strings on spaces, find bpm in first string find transposition in second string
            string[] split = filename.Split(' ');
            string nums = "";
            foreach (char c in split[1])
            {
                if (char.IsDigit(c))
                {
                    nums += c;
                }
            }
            return int.Parse(nums);
        }

        private int findTranspose(string filename)
        {
            //split strings on spaces, find bpm in first string find transposition in second string
            string[] split = filename.Split(' ');
            string nums = "";
            foreach (char c in split[2])
            {
                if (char.IsDigit(c))
                {
                    nums += c;
                }
            }
            return int.Parse(nums);
        }

        private void setBufferPtr()
        {
            unsafe
            {
                fixed (byte* FirstResult = &audioData[0])
                {
                    songBuffer.AudioDataPtr = (IntPtr)FirstResult;
                }
            }
        }

        
        public void ResetMusic()
        {
            songSource.FlushSourceBuffers();
            string songname = getSongToPlay();
            audioData = getFileData(songname);
            bpm = findBpm(songname);
            Transpose = findTranspose(songname);
            songBuffer = new XAudio2Buffer(audioData.Length);
            setBufferPtr();
            songSource.SubmitSourceBuffer(songBuffer);
            curState = songSource.GetState();
            setBeatAndBuffer();
        }

        

        #endregion
        #region Start&Stop
        public void StartTimer()
        {
            if (!updateTimer.Enabled)
            {
                updateTimer.Enabled = true;
            }
        }

        public void StopTimer()
        {
            if (updateTimer.Enabled)
            {
                updateTimer.Enabled = false;
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            SongPlayerUpdate();
        }

        public void StartSong()
        {
            StartTimer();
            songSource.Start();
        }

        public void StopSong()
        {
            StopTimer();
            songSource.Stop();
        }

        public bool HasFinishedSong()
        {
            if (SampleCount >= (audioData.Length / 4))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region InputBufferLogic

        private void setBeatAndBuffer()
        {
            SampleCount = 0;
            quarterNotesPassed = 0;
            sixteenthNotesPassed = 0;
            quarterIntervalInSamples = (((60 / bpm))* sampleRate);
            sixteenthIntervalInSamples = ((60 / bpm) / 2) * sampleRate;
            offset = quarterIntervalInSamples - Math.Truncate(quarterIntervalInSamples);
            quarterInputBuffer = ((quarterIntervalInSamples * inputBufScale));
            sixteenthInputBuffer = ((sixteenthIntervalInSamples * sixteenthInputBufScale));
        }

        bool hasAdded = false;
        public void SongPlayerUpdate()
        {
            curState = songSource.GetState();
            SampleCount += curState.SamplesPlayed - prevSampleCount;
            if (beatPassed(curState))
            {
                if (hasAdded == false)
                {
                    hasAdded = true;
                }
            }
            else
            {
                hasAdded = false;

            }
            prevSampleCount = curState.SamplesPlayed;
        }

        private RhythmState getRhythmState()
        {
            if (IsOnQuarter())
            {
                return RhythmState.Quarter;
            }
            if (IsOnSixteenth())
            {
                return RhythmState.Sixteenth;
            }
            else
            {
                return RhythmState.Offbeat;
            }
        }

     

        #region accuracyLogic
        public int SlowOrFastQuarter()
        {
            if(!IsOnQuarter()) 
            {
                if (SampleCount < (quarterIntervalInSamples * quarterNotesPassed) - quarterInputBuffer
                && SampleCount > (quarterIntervalInSamples * quarterNotesPassed) - quarterInputBuffer - (quarterBufferDistance() / 2)
                )
                {
                    return 1;
                }
                if (SampleCount < (quarterIntervalInSamples * quarterNotesPassed) - quarterInputBuffer)
                {
                    return 2;
                }
            }
            return 0;
        }

        public int SlowOrFastSixteenth()
        {
            if(!IsOnSixteenth()) {
                if (SampleCount < (sixteenthIntervalInSamples * sixteenthNotesPassed) - sixteenthInputBuffer
               && SampleCount > (sixteenthIntervalInSamples * sixteenthNotesPassed) - sixteenthInputBuffer - (sixteenthBufferDistance() / 2)
               )
                {
                    return 1;
                }
                if (SampleCount < (sixteenthIntervalInSamples * sixteenthNotesPassed) - sixteenthInputBuffer)
                {
                    return 2;
                }
            }
            return 0;
        }

        double quarterBufferDistance()
        {
            return quarterIntervalInSamples - (quarterInputBuffer * 2);
        }
        double sixteenthBufferDistance()
        {
            return sixteenthIntervalInSamples - (sixteenthInputBuffer * 2);
        }
        #endregion

        private bool IsOnSixteenth()
        {
            if (SampleCount > (sixteenthIntervalInSamples * sixteenthNotesPassed) - sixteenthInputBuffer
                && SampleCount < (sixteenthIntervalInSamples * sixteenthNotesPassed) + sixteenthInputBuffer)
            {
                return true;
            }
            return false;
        }

        private bool IsOnQuarter()
        {
            if (SampleCount > (quarterIntervalInSamples * quarterNotesPassed) - quarterInputBuffer
                && SampleCount < (quarterIntervalInSamples * quarterNotesPassed) + quarterInputBuffer)
            {
                return true;
            }
            return false;
        }

        private bool beatPassed(VoiceState curState)
        {
            if (sixteenthPassed(curState))
            {
                sixteenthNotesPassed++;
                if(hasHitQuarterBeat)
                {
                    if (!IsOnQuarter())
                    {
                        hasHitQuarterBeat = false;
                    }
                }
            }
            if (quarterPassed(curState))
            {
                quarterNotesPassed++;
                applySampleOffset();
                if (hasHitSixteenthBeat)
                {
                    if (!IsOnSixteenth())
                    {
                        hasHitSixteenthBeat = false;
                    }
                }
                return true;
            }
            return false;
        }

        private void applySampleOffset()
        {
            totalOffset += offset;
            if (totalOffset > 1)
            {
                SampleCount += 1;
                totalOffset -= 1;
            }
        }

        private bool sixteenthPassed(VoiceState curState)
        {
            if (SampleCount > (sixteenthIntervalInSamples * sixteenthNotesPassed) + (sixteenthInputBuffer+20))
            {
                return true;
            }
            return false;
        }

        private bool quarterPassed(VoiceState curState)
        {
            if(SampleCount > (quarterIntervalInSamples * quarterNotesPassed) + quarterInputBuffer)
            {
                return true;
            }
            return false;
        }

        #endregion



        //get a random string of one of the wav files
        //parse its bpm from the name
        //call setupmusic again at the end of resetmusic
        public byte[] getFileData(string fileName)
        {
            
            byte[] bytes = File.ReadAllBytes(filePath + fileName);
            byte[] chunk_id = new byte[4];
            char[] chars = {'r', 'i', 'f', 'f' };
            byte[] chunk_size= new byte[4];
            int filePtr = 0;
            for(int i =0; i < 4; i++)
            {
                chunk_id[i] = bytes[i];
                filePtr++;
            }
            for(int i =0; i < 4; i++)
            {
                if (chunk_id[i] != ((byte)chars[i])-32)
                {
                    Console.WriteLine(chunk_id[i]);
                    Console.WriteLine(((byte)chars[i])-32); 
                    Console.WriteLine("unequal");
                }
            }
            filePtr = 16;
            for (int i = filePtr; i < filePtr + 4; i++)
            {
                chunk_size[i- filePtr] = bytes[i];
                Console.WriteLine(bytes[i]);
            }
            
            for(int i = 0; i < 4; i++)
            {
                filePtr += chunk_size[i];
                Console.WriteLine(filePtr);
            }
           
            filePtr += 4;
            byte[] audioData = new byte[bytes.Length-filePtr];
            for (int i = filePtr; i < bytes.Length; i++)
            {
                audioData[i-filePtr] = bytes[i];
            }

            return audioData;
        }
    }
}
