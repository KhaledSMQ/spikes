require 'unimidi'

def send_midi_message_numbers
  notes = [36, 40, 43, 48, 52, 55, 60, 64, 67] # C E G arpeggios
  duration = 0.5

  output = UniMIDI::Output.open(:first)

  output.open do |o|
    notes.each do |note|
      o.puts(0x90, note, 100) # note on message
      sleep(duration)  # wait
      o.puts(0x80, note, 100) # note off message
    end
  end

end

require 'midi'

def send_midi_messages
  @o = UniMIDI::Output.use(:first)

  MIDI.using(@o) do

    5.times do |oct|
      octave oct + 2
      note "C", :velocity => 100 - oct * 20
      note "E", :velocity => 100 - oct * 20
      note "G", :velocity => 100 - oct * 20
      sleep(1)
      note_off "C"
      note_off "E"
      note_off "G"
    end
    #all_off

    puts cache

  end

end

def send_control_messages
  @o = UniMIDI::Output.use(:first)

  MIDI.using(@o) do
    octave 2
    3.times do
      50.times do |i|
        cc 1, i * 2
        note "C", :velocity => 127
        sleep(0.2)
        note_off "C"
      end
    end
    #puts cache

  end

end

def send_bend_messages
  @o = UniMIDI::Output.use(:first)

  MIDI.using(@o) do
    octave 4
    3.times do
      50.times do |i|
        v = i * 2
        bend 0, v
        note "C", :velocity => 127
        sleep(0.2)
        note_off "C"
      end
    end
    bend 0, 64
    #puts cache

  end

end

send_bend_messages