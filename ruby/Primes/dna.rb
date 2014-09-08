require 'rasem'

primes = []

File.open("primes.csv", "r").each_line do |line|
  primes.push(line.to_i)
end

bar_height = 2
bar_width = 9
first_prime = primes[0]
column_height = 100
column_width = 10
column_count = 30
column_origin = first_prime
h_border = column_width - bar_width
v_border = h_border

full_height = column_height * bar_height
full_width = column_width * column_count

img = Rasem::SVGImage.new(full_width + h_border, full_height + 2 * v_border - 3 * bar_height) do
  primes.each do |prime|
    puts prime
    column = (prime - first_prime) / column_height
    break if column >= column_count
    column_start = column_origin + column * column_height
    x1 = column * column_width
    y1 = (prime - column_start) * bar_height
    rectangle h_border + x1, v_border + y1, bar_width, bar_height, :stroke_width => 2, :fill => "black"
  end
end

#puts img.output
File.open("dna.svg", "w") { |file| file.write(img.output) }