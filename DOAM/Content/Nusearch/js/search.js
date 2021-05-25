$(function() {

  $("select").change(function() { $("form#search-criteria").submit(); });
  $("input[type='checkbox']").change(function() { $("form#search-criteria").submit(); });
  $(".timeago").timeago();

  setupTypeAhead();

  function setupTypeAhead() {
    var typeAheadOptions = {
      hint: true,
      highlight: true,
      minLength: 1
    };

    var remoteHandler = function(query, process) {
      return $.ajax(
          {
            cache: false,
            type: "POST",
            url: "/Suggest",
            data: JSON.stringify({ Query: query }),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
          })
        .success(function(suggestions) { process(suggestions); });
    };

    $('#query').typeahead(typeAheadOptions,
      {
        displayKey: "name",
        templates: {
          empty: [
            '<div class="lead">',
            'no suggestions found for current prefix',
            '</div>'
          ].join('\n'),
            suggestion: function (suggestion) {
                var suggestionElement = `
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <div style="width: 50px; height: 50px; min-width: 50px; background: url('${suggestion.imageUrl}'), url('${suggestion.assetTypeImage}'); background-size: cover; border-radius: 3px; margin-right: 1rem;"></div>
                            <div>
                                <div class="text-primary">${suggestion.name}</div>
                                <div style="font-size: 0.75rem; max-height: 50px; overflow: hidden;">`;
                                suggestion.tags.forEach(tag => {
                                    var text = tag.Label;
                                    suggestionElement += `<span class="badge badge-pill badge-secondary">${text}</span>`;
                                });
                suggestionElement += `
                                </div>
                            </div>
                        </div>
                        <div style="text-align: right; font-size: 0.75rem;">
                            <div>${suggestion.assetType}</div>
                            <div>${suggestion.mimeType}</div>
                        </div>
                    </div>
                `;

                

                return [suggestionElement].join('\n');
          }
        },
        source: remoteHandler
      }
    ).on('typeahead:selected', function (e, o) {
        
        window.location.href = "https://localhost:44304/Assets/Details?assetId=" + o.assetId;
      })
      .on('typeahead:selected', function(e, o) {
          $("#query").focus().select();
          console.log(o);
      });
  }

  $("#query").focus().select();

});


