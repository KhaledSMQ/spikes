require 'midi'

def play_note_by_note
  @o = UniMIDI::Output.use(:first)

  MIDI.using(@o) do

    5.times do |oct|
      octave oct + 2
      %w{C E G Bb}.each { |n| play n, 0.5 }
    end

  end
end

def play_array
  @o = UniMIDI::Output.use(:first)

  MIDI.using(@o) do

    5.times do |oct|
      octave oct + 2
      play %w{C E G Bb}, 0.5
    end

  end

end

play_note_by_note