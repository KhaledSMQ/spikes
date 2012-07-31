require 'midilib/sequence'
include MIDI

# Create a new, empty sequence.
seq = Sequence.new()

# Read the contents of a MIDI file into the sequence.
File.open('scale_fifths.mid', 'rb') { |file|
  seq.read(file) { |track, num_tracks, i|
    # Print something when each track is read.
    puts "read track #{i} of #{num_tracks}"
  }
}

seq.each { |track|
  puts "processing track '#{track.name}':"
  track.each { |event|
    puts event.to_s
  }
}