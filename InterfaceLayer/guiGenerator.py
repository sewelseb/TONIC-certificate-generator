from gooey import Gooey, GooeyParser
from argparse import ArgumentParser

@Gooey(advanced=False,              # toggle whether to show advanced config or not 
       show_config=True,            # skip config screens all together
       program_name='myProg',       # Defaults to script name
       default_size=(610, 530),     # starting size of the GUI
       required_cols=1,             # number of columns in the "Required" section
       optional_cols=2,             # number of columns in the "Optional" section
       dump_build_config=False,     # Dump the JSON Gooey uses to configure itself
       load_build_config=None,      # Loads a JSON Gooey-generated configuration
       monospace_display=False)
def main():
    parser = ArgumentParser(description="My Cool GUI Program!") 
    parser.add_argument('Filename', help="name of the file to process", widget='FileChooser')
    

key = input();
