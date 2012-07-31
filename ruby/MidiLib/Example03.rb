require 'midilib/sequence'
require 'midilib/io/seqreader'
require 'midilib/io/seqwriter'
include MIDI

# Create a new, empty sequence.
seq = MIDI::Sequence.new()

# Read the contents of a MIDI file into the sequence.
File.open('scale_fifths.mid', 'rb') { | file |
  seq.read(file) { | track, num_tracks, i |
    # Print something when each track is read.
    puts "read track #{i} of #{num_tracks}"
  }
}

# Iterate over every event in every track.
seq.each { | track |
  track.each { | event |
    # If the event is a note event (note on, note off, or poly
    # pressure) and it is on MIDI channel 1 (channels start at
    # 0, so we use 0), then transpose the event down one octave.
    if MIDI::NoteEvent === event && event.channel == 0
      event.note -= 12
    end
  }
}

# Write the sequence to a MIDI file.
File.open('scale_fifths_transposed.mid', 'wb') { | file | seq.write(file) }