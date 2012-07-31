require "test/unit"

# Any test instance variables are reset in each test method

class InstanceVariablesTest < Test::Unit::TestCase

  @instance_variable = "some value"

  def test_1_instance_variable_is_reset_to_nil_automatically_before_each_test
    assert_nil @instance_variable
  end

  def test_2_we_set_it_to_something
    @instance_variable = "something"
  end

  def test_3_but_it_is_nil_on_the_next_test
    assert_nil @instance_variable
  end

end

