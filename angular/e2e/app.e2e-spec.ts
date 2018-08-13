import { AddressBookTemplatePage } from './app.po';

describe('AddressBook App', function() {
  let page: AddressBookTemplatePage;

  beforeEach(() => {
    page = new AddressBookTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
