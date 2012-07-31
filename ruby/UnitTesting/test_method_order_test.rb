require "test/unit"

# Tests are run in alphabetical order, not in declaration order

class TestMethodOrderTest < Test::Unit::TestCase

  @@shared = ""

  def test_b
    @@shared += "b"
  end

  def test_a
    @@shared += "a"
  end

  def test_c
    @@shared += "c"
    assert_equal "abc", @@shared
  end

end